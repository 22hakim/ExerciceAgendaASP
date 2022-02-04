Correction : creation de la bdd en DBFirst 
-> apres avoir crée la base de donnée sur SQL nous devons telecharger les packages lié à EntityFramework 
-> Et taper la commande suivante

Scaffold-DbContext "Server=MON_SERVEUR;Database=NOM_DB;Trusted_Connection=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Data

Explication :
Commande EF : Scaffold-DbContext 
pour savoir quelle BDD on lie :"Server=MON_SERVEUR;Database=NOM_DB;Trusted_Connection=True" 
l'utilisation du package : 'Microsoft.EntityFrameworkCore.SqlServer 
Ou on crée les models : -OutputDir Models 
Ou on crée le fichier de connection -ContextDir Data