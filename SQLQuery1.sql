select Knihy.Nazev, Autori.Jmeno
FROM dbo.Knihy INNER JOIN dbo.Autori on Knihy.AutorId = Autori.Id;