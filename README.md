# 🐜 Swarm - ERP para Comércio e Restaurantes

**Swarm** é um **ERP** desenvolvido em **.NET** para atender **comércios e restaurantes**, oferecendo uma gestão eficiente de **produtos, estoque e emissão de NFC-e (Nota Fiscal do Consumidor Eletrônica)**.

## 🧐 Por que "Swarm"?

O nome **Swarm** (enxame, em inglês) foi escolhido porque o sistema é **estruturado como uma colônia de formigas**.  
Assim como um enxame funciona de maneira **organizada e eficiente**, cada módulo do Swarm desempenha um papel essencial no funcionamento do sistema, permitindo que diferentes setores do comércio e da gestão trabalhem de forma **integrada e otimizada**.

## 🚀 Principais Funcionalidades

✅ **Gestão de produtos e estoque** – Controle completo de entrada, saída e movimentação de itens.  
✅ **Cadastro de clientes e fornecedores** – Organização de contatos essenciais para o negócio.  
✅ **Vendas e pedidos** – Registro detalhado de pedidos e vendas.  
✅ **Emissão de NFC-e** – Integração para geração e envio de notas fiscais eletrônicas.  
✅ **Relatórios e dashboards** – Análises e métricas para tomada de decisão.  
✅ **Multiusuário** – Controle de acesso por níveis de permissão.  

## 🛠 Tecnologias Utilizadas

- **Back-end:** ASP.NET Core, Entity Framework  
- **Front-end:** Razor Pages / Blazor (ou outra tecnologia de sua escolha)  
- **Banco de Dados:** SQL Server  
- **Autenticação:** Identity Server / JWT  
- **Integração Fiscal:** API para emissão de NFC-e  

## 📌 Objetivo do Projeto

O **Swarm** foi criado para facilitar o **gerenciamento de pequenas e médias empresas**, garantindo um sistema **robusto, modular e escalável**, inspirado na eficiência da natureza.

## 🚀 Como Contribuir?

1. Faça um **fork** deste repositório.  
2. Crie uma nova branch: `git checkout -b minha-nova-feature`.  
3. Faça suas alterações e **commit**: `git commit -m 'Adicionando nova funcionalidade'`.  
4. Envie para o repositório remoto: `git push origin minha-nova-feature`.  
5. Abra um **Pull Request**!  

💡 Gostou do projeto? Deixe uma ⭐ no repositório e contribua com sugestões!
📢 Dúvidas ou feedback? Sinta-se à vontade para abrir uma Issue ou entrar em contato!

## 📂 Estrutura do Projeto

```bash
Swarm/
│── Swarm.Domain/          # Camada de domínio (entidades e regras de negócio)
│── Swarm.Application/     # Camada de aplicação (casos de uso e serviços)
│── Swarm.Infrastructure/  # Camada de infraestrutura (acesso a dados, repositórios)
│── Swarm.API/             # Camada de apresentação (API)
│── Swarm.Communication/   # Camada que contem devoluções de responses e requests
│── Swarm.Exceptions/      # Camada que contem todas as devolutivas de exceções
│── README.md              # Documentação do projeto

