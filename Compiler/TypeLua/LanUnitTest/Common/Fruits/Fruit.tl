package Common.Fruits;

import Common;

public class Fruit extends Botany

protected boolean delicious = false;

private string preserve;

public Fruit(string name)
	this.Name = name;
end

public void Eat()

end

protected boolean Preserve(string where)
	preserve = where;
	return true;
end

protected string GetPreserve()
	return preserve;
end
