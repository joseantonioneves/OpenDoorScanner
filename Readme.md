# OpenDoorScanner

## Visão Geral

O repositório **OpenDoorScanner** contém uma solução para varredura de portas TCP entre hosts, além de templates de processos de build para integração e entrega contínua em ambientes Microsoft (TFS/Azure DevOps).

## Estrutura do Projeto
* OpenDoorScanner.sln
* BuildProcessTemplates/
* AzureContinuousDeployment.11.xaml 
* DefaultTemplate.11.1.xaml 
* LabDefaultTemplate.11.xaml 
* UpgradeTemplate.xaml 
* OpenDoorScanner/ 
* App.config 
* OpenDoorScanner.csproj 
* Program.cs 
* Properties/ 
* AssemblyInfo.cs


### Descrição dos Diretórios e Arquivos

- **OpenDoorScanner.sln**  
  Arquivo de solução do Visual Studio, agrupando os projetos do repositório.

- **OpenDoorScanner/**  
  Projeto principal, implementado em C#, responsável pela varredura de portas TCP.  
  - `Program.cs`: Implementa a lógica de interação com o usuário e execução da varredura.
  - `App.config`: Configurações do aplicativo.
  - `Properties/AssemblyInfo.cs`: Informações de versão e metadados do assembly.
  - `OpenDoorScanner.csproj`: Arquivo de projeto do Visual Studio.

- **BuildProcessTemplates/**  
  Contém templates de processos de build (XAML) para automação de builds, testes e deploys em ambientes TFS/Azure DevOps.
  - `AzureContinuousDeployment.11.xaml`: Template para deploy contínuo em Azure.
  - `DefaultTemplate.11.1.xaml`: Template padrão de build, testes e publicação.
  - `LabDefaultTemplate.11.xaml`: Template para automação de builds em ambientes de laboratório.
  - `UpgradeTemplate.xaml`: Template para atualização de builds/processos.

## Finalidade do Projeto

O **OpenDoorScanner** é uma ferramenta de linha de comando para varredura de portas TCP, permitindo verificar a conectividade entre hosts em uma rede.  
O projeto é acompanhado de templates de automação de build e deploy, facilitando a integração contínua, testes automatizados e publicação em ambientes Microsoft.

## Como Usar

1. **Compilação:**  
   Abra a solução `OpenDoorScanner.sln` no Visual Studio e compile o projeto.

2. **Execução:**  
   Execute o binário gerado. O programa solicitará o IP do host e o intervalo de portas para varredura.

3. **Automação de Build:**  
   Os templates em `BuildProcessTemplates/` podem ser utilizados em pipelines do TFS/Azure DevOps para CI/CD.