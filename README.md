```markdown
# Pong Mobile Version

Repositório para o Desenvolvimento do Projeto Final da Disciplina de **Programação de Jogos em Rede**, PJD4M-2024.

---

## 🚀 Estrutura de Diretórios

### Versão da Engine do Unity: **2022.3.18f1**

```plaintext
/PONG
├── /Assets                    # Arquivos relacionados ao jogo
│   ├── /Animations             # Animações (bola, efeitos, etc.)
│   ├── /Audio                  # Sons e músicas (efeitos, fundo)
│   ├── /Fonts                  # Fontes para pontuação, títulos, etc.
│   ├── /Materials              # Materiais (texturas, shaders, etc.)
│   ├── /Prefabs                # Prefabs de objetos (paddle, bola, etc.)
│   ├── /Scenes                 # Cenas do jogo (Menu, Jogo, Game Over, etc.)
│   ├── /Scripts                # Scripts do jogo
│   │   ├── /Controllers        # Controle da lógica (pontuação, paddle)
│   │   ├── /UI                 # Interface de usuário (menus, HUD)
│   │   └── /Utils              # Scripts utilitários (gerenciamento de tempo, áudio)
│   ├── /Sprites                # Imagens e sprites (background, paddles, bola)
│   ├── /UI                     # Elementos de UI (botões, painéis)
│   └── /Videos                 # Vídeos (introdução, animações, etc.)
├── /ProjectSettings            # Configurações do projeto (não precisa alterar)
└── README.md                   # Documento com informações sobre o projeto
```

---

## 📝 Descrição

Este repositório contém o desenvolvimento do **Pong Mobile Version**, um jogo desenvolvido como parte do projeto final para a disciplina **Programação de Jogos em Rede** (PJD4M-2024). O jogo foi criado usando a **Unity**, versão **2022.3.18f1**, e é **destinado a dispositivos móveis** (Android/iOS).

### O Jogo

O **Pong** é um jogo clássico de tênis de mesa, onde o objetivo principal é controlar uma raquete (paddle) para rebater uma bola e marcar pontos contra o oponente. Nesta versão, adaptada para dispositivos móveis, a jogabilidade foi otimizada para toques e gestos, proporcionando uma experiência divertida e interativa.

#### Funcionalidades:
- **Modos de Jogo**: Inclui diferentes modos, como jogo solo contra a IA e multiplayer local.
- **Controles Tácteis**: Suporte para toques e gestos, ideais para dispositivos móveis.
- **Interface de Usuário (UI)**: Menu inicial, HUD de jogo, tela de game over com pontuação.
- **Pontuação e Desafios**: O jogo registra pontos e desafios, com animações e efeitos sonoros.

---

## ⚙️ Como Executar o Projeto

Para executar este projeto, siga os passos abaixo:

1. **Baixe** ou faça o **clone** deste repositório.
2. Abra o projeto no **Unity** com a versão **2022.3.18f1**.
3. Navegue até a pasta **`/Scenes`** e abra a cena desejada (por exemplo: `MainMenu`, `Game`, `GameOver`).
4. Clique em **Play** para testar o jogo no editor do Unity ou **exporte** para dispositivos móveis (Android/iOS).

---

## 📦 Contribuições

Se deseja contribuir para o projeto, siga estas etapas:

1. **Fork** este repositório.
2. Crie uma **branch** para a sua feature:  
   `git checkout -b minha-feature`
3. **Commit** suas alterações:  
   `git commit -am 'Adiciona nova feature'`
4. **Push** para a branch:  
   `git push origin minha-feature`
5. Abra um **Pull Request** para revisão.

---

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT**. 
```