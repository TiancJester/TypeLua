typelua = {}

local modules = {}

local loading = {}
local refdepth = 0
local doload = 0

function tlload(name)
    if modules[name] then
        return modules[name]
    end

    refdepth = refdepth + 1
    --print("load start "..name.." depth "..refdepth)
    modules[name] = require(name)
    modules[name]._loadreference()
    table.insert(loading,name)
    refdepth = refdepth - 1
    --print("load end "..name.." depth "..refdepth)

    if refdepth == 0 then
        for i = 1,#loading do
            local className = loading[i]
            local newclass = modules[className]

            --print("call static ctor "..tostring(newclass).." "..tostring(newclass.__classname))

            tlextends(newclass)
            if newclass._staticctor then
                newclass._staticctor()
            end
            loading[i] = nil
        end
    end

    return modules[name]
end

function tlunload(name)
    modules[name] = nil
end

function tlclass(classname,super)
    local newclass = {}

    newclass.__supername = super
    newclass.__classname = classname
    newclass.__index = newclass


    newclass.new = function(...)
		local superclass = setmetatable({}, {__index = newclass.__super})
		local selfclass = setmetatable({}, newclass)
		selfclass.super = superclass

        local o = setmetatable({}, {__index = selfclass})
		createSelfContext(o,o,o,o._methods)

		if o.__super ~= nil then
			--local super = {}
			--setmetatable(super, {__index = o.__super})
			--o.super = super

			local c = o.__super
			--print(classname..tostring(c.__super))
			while c ~= nil do
				createSelfContext(o,superclass,selfclass ,c._methods)
				createSelfContext(o,superclass,superclass ,c._methods)
				c = c.__super
				--print(c.__classname.." parent is ",super.__classname)
			end
		end

        o._ctor(o,...)
        return o
    end

    return newclass
end

function tlmethod(class,...)
    class._methods = {...}
end

function tlextends(newclass)
    --print(newclass.__classname .. " extends "..tostring(newclass.__supername))
    if newclass.__supername ~= nil then
        local super = tlload(newclass.__supername)
        setmetatable(newclass, {__index = super})
        newclass.__super = super
    end

    if newclass._ctor == nil then
        newclass._ctor = function() end
    end
end

function createSelfContext(self,functable, overridetable,methods)
    if methods ~= nil then
		local selfcontext = {}
		local originalcontext = {}
		for i = 1, #methods do
			local funcName = methods[i]
			originalcontext[funcName] = functable[funcName]
			selfcontext[funcName] = function(...)
				return originalcontext[funcName](self,...)
			end
			overridetable[funcName] = selfcontext[funcName]
		end
	end
end


