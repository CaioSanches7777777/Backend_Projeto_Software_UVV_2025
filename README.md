# Wiki_LabEngenharia
## Tabelas

```mermaid
classDiagram

class usuario{
- id_usuario INTEGER NOT NULL DEFAULT nextval('public.usuario_id_usuario_seq')
- nome VARCHAR(100) NOT NULL
- senha VARCHAR(300) NOT NULL
- endereco_email VARCHAR(150) NOT NULL
- imagem VARCHAR
 CONSTRAINT pk_usuario PRIMARY KEY (id_usuario)
}

class paginas{
- id_pagina: INTEGER NOT NULL DEFAULT nextval('public.paginas_id_pagina_seq')
- id_wiki: INTEGER NOT NULL
- nsfw_flag: BOOLEAN NOT NULL
- conteudo: TEXT NOT NULL
- is_main: BOOLEAN NOT NULL
CONSTRAINT pk_paginas PRIMARY KEY (id_pagina, id_wiki)
}

class wiki{
- id_wiki INTEGER NOT NULL DEFAULT nextval('public.wiki_id_wiki_seq_1')
- nome VARCHAR(100) NOT NULL
- descricao VARCHAR(600)
- endereco_web VARCHAR(100) NOT NULL
- id_tag INTEGER NOT NULL
CONSTRAINT pk_wiki PRIMARY KEY (id_wiki)
}

class tags{
- id_tag INTEGER NOT NULL DEFAULT nextval('public.tags_id_tag_seq_1')
- nome VARCHAR(50) NOT NULL
- descricao VARCHAR(300)
CONSTRAINT pk_tags PRIMARY KEY (id_tag)
}

class moderadores{
- id_moderadores INTEGER NOT NULL DEFAULT nextval('public.moderadores_id_moderadores_seq')
- id_wiki INTEGER NOT NULL
- id_usuario INTEGER NOT NULL
 CONSTRAINT pk_moderadores PRIMARY KEY (id_moderadores, id_wiki, id_usuario)
}

class assinatura{
- id_assinatura INTEGER NOT NULL DEFAULT nextval('public.assinatura_id_assinatura_seq')
- plano VARCHAR(20) DEFAULT 'GRATUITO' NOT NULL
- preco DOUBLE PRECISION NOT NULL
- tempo_duracao INTEGER NOT NULL
 CONSTRAINT pk_assinatura PRIMARY KEY (id_assinatura)
}

class aquisicao_assinatura{
- id_aquisicao INTEGER NOT NULL DEFAULT nextval('public.aquisicao_assinatura_id_aquisicao_seq')
- id_assinatura INTEGER NOT NULL
- id_usuario INTEGER NOT NULL
- renovar_automatico BOOLEAN NOT NULL
- data_inicio DATE NOT NULL
- data_fim DATE NOT NULL
 CONSTRAINT pk_aquisicao PRIMARY KEY (id_aquisicao, id_assinatura, id_usuario)
}

usuario "1"-->"1" moderadores
assinatura "1"-->"0/N" aquisicao_assinatura
aquisicao_assinatura "1"-->"1" usuario
tags "1/N"-->"0/N" wiki
wiki "1"-->"1/N" paginas
wiki "1"-->"1/N" moderadores
```
### Observações:
Configurar o localhost do banco de dados em appsettings.json

Configurar a chave secreta do JWT no appsettings.json

Quando estiver conectado ao banco de dados, descomentar a função em RegisterService.cs:
* /*if (await _db.Users.AnyAsync(u => u.Email == dto.Email)) 
throw new ApplicationException("E-mail já cadastrado.");*/


## Instale as bibliotecas dos pacotes para net8.0:

```dotnet
dotnet add package BCrypt.Net-Next
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Authorization
dotnet add package Microsoft.AspNetCore.Mvc.RazorPages
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Swashbuckle.AspNetCore
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package System.Text.RegularExpressions
```

Caso haja erros por causa de versões incompatíveis, acesse o gerenciador de pacotes NuGet do visual studio:
<img width="1918" height="1020" alt="image" src="https://github.com/user-attachments/assets/91a694d4-dd4e-42d1-82fe-8f1f595f893a" />

# Use essas versões:

 BCrypt.Net-Next                                       4.0.3

 Microsoft.AspNetCore.Authentication.JwtBearer         8.0.14

 Microsoft.AspNetCore.Authorization                    8.0.0

 Microsoft.AspNetCore.Mvc.RazorPages                   2.3.0

 Microsoft.EntityFrameworkCore                         9.0.9

 Microsoft.IdentityModel.Tokens                        8.14.0

 Npgsql.EntityFrameworkCore.PostgreSQL                 9.0.4

 Swashbuckle.AspNetCore                                8.1.4

 System.IdentityModel.Tokens.Jwt                       8.14.0

 System.Text.RegularExpressions                        4.3.1
