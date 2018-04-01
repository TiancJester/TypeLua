package Common;

public class Land

public static string Instructions = "some words.";

public string Name;

public Farmer Owner;

protected Botany plantedBotany;

public Land(string landName)
	this.Name = landName;
end

public void Plant(Botany botany)
	plantedBotany = botany;
end

public Botany GetPlantedBotany()
	return plantedBotany;
end

public string,Farmer,Botany GetInfo()
	Botany p;
	if plantedBotany == nil then
		p = new Botany();
	else
		p = plantedBotany;
	end
	return Name,Owner,p;
end
