using System;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot(ElementName = "head")]
public class Head
{

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "script_enable")]
    public bool ScriptEnable { get; set; }
}

[XmlRoot(ElementName = "torso")]
public class Torso
{

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "script_enable")]
    public bool ScriptEnable { get; set; }
}

[XmlRoot(ElementName = "pants")]
public class Pants
{

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "script_enable")]
    public bool ScriptEnable { get; set; }
}

[XmlRoot(ElementName = "shorts")]
public class Shorts
{

    [XmlElement(ElementName = "name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "script_enable")]
    public bool ScriptEnable { get; set; }
}

[XmlRoot(ElementName = "body")]
public class Body
{

    [XmlElement(ElementName = "head")]
    public Head Head { get; set; }

    [XmlElement(ElementName = "torso")]
    public Torso Torso { get; set; }

    [XmlElement(ElementName = "pants")]
    public Pants Pants { get; set; }

    [XmlElement(ElementName = "shorts")]
    public Shorts Shorts { get; set; }
}


[XmlRoot(ElementName = "personagem_1")]
public class Personagem1
{

    [XmlElement(ElementName = "namePerson")]
    public string Name { get; set; }
    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "personagem_2")]
public class Personagem2
{

    [XmlElement(ElementName = "namePerson")]
    public string Name { get; set; }
    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "personagem_3")]
public class Personagem3
{

    [XmlElement(ElementName = "namePerson")]
    public string Name { get; set; }
    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "personagem_4")]
public class Personagem4
{

    [XmlElement(ElementName = "namePerson")]
    public string Name { get; set; }
    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "personagem_5")]
public class Personagem5
{

    [XmlElement(ElementName = "namePerson")]
    public string Name { get; set; }
    [XmlElement(ElementName = "body")]
    public Body Body { get; set; }
}

[XmlRoot(ElementName = "personList")]
public class PersonList
{
    [XmlElement(ElementName = "personagem_1")]
    public Personagem1 Personagem1 { get; set; }

    [XmlElement(ElementName = "personagem_2")]
    public Personagem2 Personagem2 { get; set; }

    [XmlElement(ElementName = "personagem_3")]
    public Personagem3 Personagem3 { get; set; }

    [XmlElement(ElementName = "personagem_4")]
    public Personagem4 Personagem4 { get; set; }

    [XmlElement(ElementName = "personagem_5")]
    public Personagem5 Personagem5 { get; set; }
}
