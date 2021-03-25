select Knihy.Nazev as 'nazev_knihy'
from dbo.Knihy inner join dbo.Zaci_has_Knihy on Knihy.Id = Zaci_has_Knihy.IdKnihy
WHERE Zaci_has_Knihy.IdZaka = 4008
;

select Knihy.Nazev, Autori.Jmeno
FROM dbo.Knihy INNER JOIN dbo.Autori on Knihy.AutorId = Autori.Id;