# 🌍 Museu Imersivo do Sistema Solar

<div align="center">

![Sistema Solar VR](https://img.shields.io/badge/Unity-6000.1.4f1+-blue?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/C%23-12.0-brightgreen?style=for-the-badge&logo=c-sharp)
![Realidade Virtual](https://img.shields.io/badge/VR-Gaze%20Detection-purple?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-80%25%20MVP-yellow?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**Um museu virtual interativo em Realidade Virtual para exploração educativa do sistema solar**

[Sobre](#sobre) • [Características](#características) • [Instalação](#instalação) • [Como Usar](#como-usar) • [Tecnologias](#tecnologias) • [Autores](#autores)

</div>

---

## 📚 Sobre

Museu Imersivo do Sistema Solar é um ambiente de Realidade Virtual desenvolvido em Unity 6 que permite aos usuários explorar os 9 planetas do sistema solar de forma imersiva e educativa. 

O projeto implementa **detecção de olhar (gaze detection)** como principal mecanismo de interação, criando uma experiência **100% acessível e hands-free** (sem necessidade de controladores).

### 🎯 Objetivo

Tornar a educação científica mais imersiva, acessível e memorável através de Realidade Virtual, eliminando barreiras de acessibilidade para usuários com diferentes capacidades.

---

## ✨ Características

### 🔬 Detecção de Olhar (Gaze Detection)
- Sistema de raycast contínuo a partir da câmera
- Timer intuitivo de 2 segundos para ativar interação
- Feedback visual em tempo real (círculo preenchendo)
- Indicador visual dinâmico (crosshair)

### 🎥 Câmera Adaptativa
- Posiciona-se automaticamente para cada planeta
- Funciona perfeitamente para todos os 9 planetas (sem ajuste individual)
- Sempre posicionada no lado bem iluminado pelo Sol
- Zoom cinematográfico suave (0.5 segundos)

### 🪐 Exploração dos 9 Planetas
- **Mercúrio** - O planeta mais rápido
- **Vênus** - O planeta gêmeo da Terra
- **Terra** - Nosso lar azul com Lua visível
- **Marte** - O planeta vermelho
- **Júpiter** - O gigante do sistema
- **Saturno** - Com anéis 3D espetaculares
- **Urano** - O planeta inclinado
- **Netuno** - O planeta ventoso e azul
- **Plutão** - O pequeno planeta anão

Cada planeta possui:
- ✓ Dados científicos verificados
- ✓ Descrição detalhada
- ✓ 3+ curiosidades educacionais
- ✓ Rotações realistas
- ✓ Sistema de narração automática (pronto para áudios)

### 📊 Painel Informativo
- Nome do planeta em destaque
- Descrição científica completa
- Curiosidades fascinantes
- Interface limpa com TextMeshPro
- Fundo semi-transparente

### 🎮 Fluxo 100% Hands-Free
1. Você olha para um planeta
2. Mantém o foco por 2 segundos
3. Câmera faz zoom automaticamente
4. Painel informativo aparece
5. Narração começa a tocar
6. Ao terminar, tudo retorna automaticamente

### 🔄 Pausa Inteligente de Órbitas
- Órbitas pausam durante exploração
- Rotação local dos planetas continua
- Mantém dinamismo visual realista

---

## 🛠️ Tecnologias

| Categoria | Tecnologia |
|-----------|-----------|
| **Engine** | Unity 6 (6000.1.4f1+) |
| **Linguagem** | C# |
| **Renderização** | Universal Render Pipeline (URP) |
| **UI** | TextMeshPro (TMP) |
| **Física** | Raycasting |
| **Plataforma** | Windows/Desktop (Mobile planejado) |
| **Versionamento** | Git/GitHub |

---

## 📊 Estatísticas do Desenvolvimento

| Métrica | Valor |
|---------|-------|
| **Scripts Especializados** | 8 |
| **Problemas Resolvidos** | 13+ |
| **Erros Reduzidos** | 999+ → 0 |
| **Planetas Funcionais** | 9/9 (100%) |
| **Progresso MVP** | 80% |
| **Dados Científicos** | 100% |

---

## 🚀 Instalação

### Pré-requisitos
- Unity 6 (versão 6000.1.4f1 ou superior)
- C# (está incluído no Unity)
- Git (para clonar o repositório)

### Passos

1. **Clone o repositório**
```bash
git clone https://github.com/cadu-ers/-MUSEU-IMERSIVO-DO-SISTEMA-SOLAR.git
cd Planetarium-VR
```

2. **Abra em Unity**
- Abra Unity Hub
- Clique em "Open Project"
- Selecione a pasta do projeto

3. **Espere o projeto carregar**
- Unity irá importar todos os assets
- Compilará os scripts
- Pode levar alguns minutos na primeira vez

4. **Pronto!**
- Abra a cena principal em `Assets/Scenes/`
- Pressione Play para testar

---

## 💻 Como Usar

### Na Editor Unity

1. **Pressione Play** (ou Ctrl+P)
2. **Mova o mouse** para explorar (ou use Mouse Look)
3. **Olhe para um planeta** com o crosshair no centro
4. **Mantenha o foco por 2 segundos** - veja o círculo verde preenchendo
5. **Câmera faz zoom automaticamente**
6. **Painel informativo aparece** com dados do planeta
7. **Narração toca** (quando estiver gravada)
8. **Ao terminar**, tudo volta automaticamente

### Controles

| Input | Ação |
|-------|------|
| **Mouse** | Rotacionar câmera / Explorar |
| **Olhar (Gaze)** | Olhar para planeta por 2s |
| **Círculo Verde** | Indicador de progresso de interação |

---

## 📁 Estrutura do Projeto
