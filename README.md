﻿# Controle De Medicamentos
![Status](https://img.shields.io/badge/status-em_desenvolvimento-yellow)

![banner](https://i.imgur.com/vdeYeV6.png)

# 📌 Demonstração
 Navegações e cadastros
>![Demonstração do Projeto](https://i.imgur.com/WrLEsbs.gif)

# 💡 Índice

- [Demonstração](#-demonstração)
- [Introdução](#-introdução)
- [Funcionalidades](#-funcionalidades)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Arquivos Relacionados](#-arquivos-relacionados)
- [Tecnologias Usadas](#-tecnologias-usadas)
- [Commits e Convenções](#-commits-e-convenções)
- [Como Usar](#-como-usar)
- [Contribuidores](#-contribuidores)
- [Mentores](#-mentores)
- [Sobre o Projeto](#-sobre-o-projeto)
- [FeedBack](#-feedback)
- [Como Contribuir](#-como-contribuir)

# 📚 Introdução

O **Controle de Medicamentos** é uma aplicação de console desenvolvida em C# com .NET 8.0, focada na gestão de medicamentos e pacientes. O objetivo é permitir o controle de entrada e saída de medicamentos, vinculando-os a pacientes cadastrados, de maneira clara e persistente.


# ✨ Funcionalidades

- 💾 **Persistência via JSON** – Todos os dados são armazenados de forma local e persistente.
- 🗂️ **Gerenciamento Completo de Registros** – Cadastro, edição, exclusão e listagem de pacientes, medicamentos, funcionários, fornecedores, requisições e prescrições médicas.
- 📥📤 **Controle de Estoque** – Registra entradas e saídas de medicamentos com rastreamento por funcionário e paciente.
- 🧾 **Validações de Entrada** – Campos obrigatórios, tipos e formatos verificados para garantir consistência dos dados.
- 🎛️ **Menu Interativo com Teclado** – Navegação fluida com setas e seleção por Enter, sem necessidade de digitar números.
- 📊 **Listagens com Destaques Visuais** – Interface colorida destaca status como “Em falta”, “Ok”, “Aberta” ou “Expirada”.



## 🧱 Estrutura do Projeto

O projeto está organizado em **módulos**, cada um responsável por uma funcionalidade distinta do sistema. Cada módulo segue uma estrutura padrão composta por:

- `Entidade` – Classe que representa o dado principal.
- `Repositório` – Responsável pela persistência e manipulação dos dados em arquivos JSON.
- `Tela` – Interface de interação no console com o usuário.

Há também um **módulo base** que centraliza comportamentos comuns, permitindo reutilização e padronização entre os módulos.

Exemplo de organização:

- 📁 `BaseModule/`
  - `BaseEntity.cs` – Entidade abstrata com propriedades comuns.
  - `BaseRepository.cs` – Repositório genérico com lógica comum para leitura e gravação em JSON, utilizado por todos os repositórios específicos.
  - `BaseScreen.cs` – Tela base com estrutura de menu e exibição reutilizada por outros módulos.

  
- 📁 `PatientModule/`
  - `Patient.cs` – Entidade com os dados do paciente.
  - `PatientRepository.cs` – Repositório que herda de `BaseRepository`.
  - `PatientScreen.cs` – Tela com opções para gerenciar pacientes.

- 📁 `Program.cs` – Ponto de entrada principal. Inicializa o menu principal e roteia para os módulos.

Essa divisão modular, com herança de comportamentos comuns, torna o projeto **escalável, reutilizável e fácil de manter**.

# 📁 Arquivos Relacionados

- [📄 Documento de Requisitos](https://github.com/Code-Oblivion/Controle-de-Medicamentos/blob/master/Controle-de-Medicamentos.ConsoleApp/docs/Revisado%20-%20Controle%20de%20Medicamentos%20-%202025.pdf)
- [🧠 Diagrama de Classes](https://github.com/Code-Oblivion/Controle-de-Medicamentos/blob/master/Controle-de-Medicamentos.ConsoleApp/docs/diagram.png)
- [🗺️ Organização](https://github.com/Code-Oblivion/Controle-de-Medicamentos/blob/master/Controle-de-Medicamentos.ConsoleApp/docs/organizacao.png)

# 🔧 Tecnologias usadas

[![Tecnologias](https://skillicons.dev/icons?i=git,github,cs,dotnet,visualstudio)](https://skillicons.dev)

# 🧠 Commits e Convenções

Utilizamos [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0/) para padronizar as mensagens de commit.

# 💾 Como Usar

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.

### 1. Clone o repositório.
 
```
git clone https://github.com/Code-Oblivion/Controle-de-Medicamentos.git
```
### 2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz

```
cd Controle-de-Medicamentos
```

### 3. Utilize o comando abaixo para restaurar as dependências do projeto.

```
dotnet restore
```

### 4. Navegue até a pasta do projeto

```
cd Controle-de-Medicamentos.ConsoleApp
```

### 5. Execute o projeto

```
dotnet run
```

# 👥 Contribuidores

<p align="left">
  <a href="https://github.com/AgathaSates">
    <img src="https://github.com/AgathaSates.png" width="100" style="border-radius: 50%;" alt="Tiago Santini"/>
  </a>
  &nbsp;&nbsp;&nbsp;
  <a href="https://github.com/otaviobrignoni">
    <img src="https://github.com/otaviobrignoni.png" width="100" style="border-radius: 50%;" alt="Alexandre Rech"/>
  </a>
    &nbsp;&nbsp;&nbsp;
  <a href="https://github.com/AlexAraldi">
    <img src="https://github.com/AlexAraldi.png" width="100" style="border-radius: 50%;" alt="Alexandre Rech"/>
  </a>
</p>

| Nome | GitHub |
|------|--------|
| Agatha Sates | [@AgathaSates](https://github.com/AgathaSates) |
| Otavio Brignoni | [@Otavio Brignoni](https://github.com/otaviobrignoni) |
| Alexander Araldi | [@Alexander Araldi](https://github.com/AlexAraldi) |

# 👨‍🏫 Mentores

<p align="left" style="margin-left: 27px;">
  <a href="https://github.com/tiagosantini">
    <img src="https://github.com/tiagosantini.png" width="100" style="border-radius: 50%;" alt="Tiago Santini"/>
  </a>
  &nbsp;&nbsp;&nbsp;
  <a href="https://github.com/alexandre-rech-lages">
    <img src="https://github.com/alexandre-rech-lages.png" width="100" style="border-radius: 50%;" alt="Alexandre Rech"/>
  </a>
</p>


| Nome | GitHub |
|------|--------|
| Tiago Santini | [@Tiago Santini](https://github.com/tiagosantini) |
| Alexandre Rech | [@Alexandre Rech](https://github.com/alexandre-rech-lages) |

# 🏫 Sobre o Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://academiadoprogramador.net) 2025

# 💬 Feedback

Se você tiver sugestões de melhoria, novas ideias ou quiser nos avisar sobre um bug, abra uma [Issue](https://github.com/Code-Oblivion/Controle-de-Medicamentos/issues) ou entre em contato!

# 🤝 Como Contribuir

1. 🍴 Faça um fork
2. 🛠️ Crie uma branch `feature/sua-feature`
3. 🔃 Commit com mensagens semânticas (`feat: nova tela`)
4. 📥 Abra um Pull Request e aguarde o review
