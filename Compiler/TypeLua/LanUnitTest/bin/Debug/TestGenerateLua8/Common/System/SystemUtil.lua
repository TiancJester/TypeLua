--== start class define ==--
local SystemUtil = tlclass("TestGenerateLua8.Common.System.SystemUtil")

--== require modules ==--
local luautil

function SystemUtil._loadreference()
    luautil = require("TestGenerateLua8.Common.System.luautil")
end
--== static constructor ==--
function SystemUtil._staticctor()

    json = nil

end
--== global functions ==--



--== constructor ==--
function SystemUtil:_ctor()
end
--== end class define ==--
return SystemUtil
