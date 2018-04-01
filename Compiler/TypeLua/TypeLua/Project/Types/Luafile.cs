// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>15/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    public class Luafile
    {
        public Project Project;
        
        public string FilePath;

        public string FullName;

        public string Name;


        public Luafile(Project project, string path)
        {
            this.Project = project;
            this.FilePath = path;
            this.FullName = path.Replace("/", ".");
            var names = this.FullName.Split('.');
            this.Name = names[names.Length - 1];
        }
    }
}