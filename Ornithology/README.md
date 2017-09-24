# Paquetes

EntityFramework.SqlServerCompact 6.1.3

#Instancia de Database

(LocalDB)\MSSQLLocalDB

# Comandos utiles

## Reinstalación de Paquetes

Seleccione el proyecto deseado en Package Manager Console y ejecute

`Update-Package -reinstall`

## Activar Migrations en proyecto Data

`Enable-Migrations -ProjectName Ornithology.Data -StartUpProjectName Ornithology.WebApi -Verbose`


