// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>12/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;

    public class ClassParser
    {
        public void BuildGlobalContext(Class tlClass)
        {
            var packagesContext = tlClass.Packages;
            foreach (var c in packagesContext.GetClasses())
            {
                foreach (var contextElement in c.Methods.GetAllElements().Values)
                {
                    var function = contextElement as Function;
                    if ((function.Access & AccessType.Global) > 0)
                    {
                        tlClass.Global.AddElement(function);
                    }
                }
                foreach (var contextElement in c.Fields.GetAllElements().Values)
                {
                    var field = contextElement as Field;
                    if ((field.Access & AccessType.Global) > 0)
                    {
                        tlClass.Global.AddElement(field);
                    }
                }
            }
        }

        public void ClarifyType(Class tlClass)
        {
            var packagesContext = tlClass.Packages;
            if (!string.IsNullOrEmpty(tlClass.BaseClassName))
            {
                var baseClass = packagesContext.GetClass(tlClass.BaseClassName);
                if (baseClass == null)
                {
                    var positionToken = tlClass.BaseClassProduction.GetPositionToken(null);
                    throw new SyntaxException("Base class not found.", positionToken.Line, positionToken.Column);
                }
                tlClass.BaseClass = baseClass;
            }

            var fields = tlClass.Fields.GetAllElements();
            foreach (var value in fields.Values)
            {
                this.ClarifyType(value as Field, packagesContext);
            }

            this.ClarifyType(tlClass.ClassConstructor, packagesContext);

            var methods = tlClass.Methods.GetAllElements();
            foreach (var value in methods.Values)
            {
                this.ClarifyType(value as Function, packagesContext);
            }

            var globalElements = tlClass.Global.GetAllElements();
            foreach (var value in globalElements.Values)
            {
                if (value.ElementCategory == ContextElementCategory.Field)
                {
                    this.ClarifyType(value as Field, packagesContext);
                }
                else if (value.ElementCategory == ContextElementCategory.Function)
                {
                    this.ClarifyType(value as Function, packagesContext);
                }
            }
        }

        public void TypeVerify(Class tlClass)
        {
            if (tlClass.ClassConstructor != null)
            {
                tlClass.ClassConstructor.Body.ContextVerify(tlClass.ClassConstructor);
            }
            if (tlClass.StaticConstructor != null)
            {
                tlClass.StaticConstructor.Body.ContextVerify(tlClass.StaticConstructor);
            }
            foreach (var contextElement in tlClass.Methods.GetAllElements().Values)
            {
                var function = contextElement as Function;
                if (function.Body != null)
                {
                    function.Body.ContextVerify(function);
                }
            }
            foreach (var value in tlClass.Fields.GetAllElements().Values)
            {
                var field = value as Field;
                field.DefineProduction.ContextVerify(tlClass);
            }
        }

        private void ClarifyType(Field field, PackagesContext packagesContext)
        {
            try
            {
                field.Type.ClarifyType(packagesContext);
            }
            catch (UnknowTypeException e)
            {
                var positionToken = field.DefineProduction.GetPositionToken();
                throw new SyntaxException(e.Message, positionToken.Line, positionToken.Column);
            }
        }
        private void ClarifyType(Function function, PackagesContext packagesContext)
        {
            if (function == null)
            {
                return;
            }
            if (function.Parameters != null)
            {
                foreach (var parameter in function.Parameters)
                {
                    parameter.Type.ClarifyType(packagesContext);
                }
            }
            if (function.ReturnValueTypes != null)
            {
                foreach (var returnValueType in function.ReturnValueTypes)
                {
                    returnValueType.ClarifyType(packagesContext);
                }
            }
        }
    }
}