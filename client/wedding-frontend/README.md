# Wedding Website - Astro + Tailwind CSS

Este é um projeto de site de casamento desenvolvido com Astro e Tailwind CSS para fins educacionais, focando na organização de pastas e estrutura de código.

## 🚀 Estrutura do Projeto

```
/
├── public/
│   └── favicon.svg
├── src/
│   ├── components/
│   │   ├── ui/
│   │   │   ├── Button.astro
│   │   │   ├── Card.astro
│   │   │   └── Navigation.astro
│   │   └── sections/
│   │       ├── HeroSection.astro
│   │       ├── WelcomeSection.astro
│   │       ├── CountdownSection.astro
│   │       ├── StorySection.astro
│   │       ├── DetailsSection.astro
│   │       ├── DressCodeSection.astro
│   │       ├── GallerySection.astro
│   │       └── GiftsSection.astro
│   ├── data/
│   │   └── wedding-data.js
│   ├── layouts/
│   │   └── Layout.astro
│   ├── pages/
│   │   └── index.astro
│   ├── styles/
│   │   └── global.css
│   └── utils/
│       └── date-utils.js
└── package.json
```

## 🧞 Comandos

Todos os comandos são executados na raiz do projeto, no terminal:

| Comando                   | Ação                                           |
| :------------------------ | :----------------------------------------------- |
| `npm install`             | Instala as dependências                        |
| `npm run dev`             | Inicia o servidor de desenvolvimento local em `localhost:4321`      |
| `npm run build`           | Constrói o site de produção para `./dist/`          |
| `npm run preview`         | Visualiza sua build localmente, antes de fazer deploy         |
| `npm run astro ...`       | Executa comandos CLI como `astro add`, `astro check` |
| `npm run astro -- --help` | Obtém ajuda usando o CLI do Astro                     |

## 📁 Organização das Pastas

### `/src/components/`
- **`ui/`**: Componentes reutilizáveis de interface (Button, Card, Navigation)
- **`sections/`**: Seções específicas da página (Hero, Welcome, Countdown, etc.)

### `/src/data/`
- Contém dados estruturados do casamento (informações do casal, local, dress code, etc.)

### `/src/layouts/`
- Templates de layout base para as páginas

### `/src/pages/`
- Páginas do site (Astro usa roteamento baseado em arquivos)

### `/src/styles/`
- Estilos globais e configurações do Tailwind CSS

### `/src/utils/`
- Funções utilitárias (formatação de datas, cálculos, etc.)

## 🎨 Design System

### Cores
- **Primária**: Tons de slate (azul acinzentado)
- **Secundária**: Branco e tons de cinza claro
- **Accent**: Azul serenidade

### Tipografia
- **Fonte principal**: System fonts (Inter, Segoe UI, etc.)
- **Fonte decorativa**: Dancing Script (para títulos especiais)

### Componentes
- **Button**: Variações primary, secondary, outline
- **Card**: Container com sombra e bordas arredondadas
- **Navigation**: Menu responsivo com indicadores de seção ativa

## 🛠️ Tecnologias Utilizadas

- **Astro**: Framework web moderno para sites rápidos
- **Tailwind CSS**: Framework CSS utilitário
- **JavaScript**: Para interatividade (countdown, navegação)
- **Google Fonts**: Dancing Script para tipografia decorativa

## 📱 Responsividade

O site é totalmente responsivo e otimizado para:
- Desktop (1024px+)
- Tablet (768px - 1023px)
- Mobile (320px - 767px)

## ✨ Funcionalidades

- **Navegação suave**: Scroll suave entre seções
- **Countdown dinâmico**: Contagem regressiva em tempo real
- **Menu responsivo**: Menu mobile com toggle
- **Seção ativa**: Indicador visual da seção atual
- **Cards interativos**: Hover effects e transições
- **Galeria de fotos**: Placeholder para fotos futuras
- **Lista de presentes**: Sistema de contribuições organizadas

## 🎯 Objetivos Educacionais

Este projeto demonstra:
1. **Organização de componentes** em Astro
2. **Separação de responsabilidades** (UI, sections, data, utils)
3. **Reutilização de componentes**
4. **Gerenciamento de estado** com JavaScript vanilla
5. **Design responsivo** com Tailwind CSS
6. **Estrutura de dados** centralizada
7. **Boas práticas** de desenvolvimento frontend

## 📝 Notas

- O projeto está configurado para desenvolvimento local
- As imagens da galeria são placeholders
- Os botões de pagamento são apenas demonstrativos
- O countdown é calculado dinamicamente baseado na data do casamento

## 🤝 Contribuições

Este é um projeto educacional. Sinta-se livre para usar como base para seus próprios projetos ou para aprender sobre Astro e Tailwind CSS.

