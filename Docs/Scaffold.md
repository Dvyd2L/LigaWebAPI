# Scaffold

> ScaffoldTemplate

Scaffold-DbContext 
"Data Source=<BDServer>;Initial Catalog=<BDName>;Integrated Security=<Boolean>;TrustServerCertificate=<Boolean>" 
-Provider <BDEngine> 
-OutputDir <outFolder>
-force 
-project <ProjectName>

> ScaffoldProject

Scaffold-DbContext "Data Source=PC_CELIA_DAVID;Initial Catalog=Liga;Integrated Security=True;TrustServerCertificate=True" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force -project EvaluacionDavidLlopis