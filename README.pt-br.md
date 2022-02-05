# Scripts

Se eu te ajudei e você quer doar qualquer quantia:  
[![Doar](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY)  
Pix: bhenrike@prontonmail.com

- [English version](README.md)

- [RBot Docs](https://brenohenrike.github.io/Scripts/)

- [Scripts](#Scripts)
  - [A fazer](#a-fazer)
  - [Customizando CoreBots](#customizando-corebots)
  - [Core Skill Plugin](#core-skill-plugin)
  - [FAQ](#faq)

## A fazer

- A meta de uma interface mais fácil de utilizar foi atingida, **RBot 4** deve sair pelo **fim de Fevereiro/Março**. Obrigado a todos que doaram. ([Você ainda pode doar aqui.](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY))

## Customizando CoreBots

No arquivo **CoreBots.cs** você achará diversas propriedades que podem se mudadas a sua preferência, o valor padrão delas esta listado abaixo:

```csharp
// [Pode Mudar] Tempo de espera entre comandos, 700ms é o padrão
public int ActionDelay { get; set; } = 700;
// [Pode Mudar] Tempo de espera para sair de combate, 1600ms é o padrão
public int ExitCombatDelay { get; set; } = 1600;
// [Pode Mudar] Tempo de espera entre matar um monstro e pular para outra sala quando estiver usando Hunt, aumente o valor se achar que esta pulando muito rápido
public int HuntDelay { get; set; } = 1000;
// [Pode Mudar] Quantas packets de aceitar/completar a quest serão enviadas
public int AcceptandCompleteTries { get; set; } = 20;
// [Pode Mudar] Se os bots vão usar salas privadas
public bool PrivateRooms { get; set; } = true;
// [Pode Mudar] Se o player deve dar rest depois de matar um monstro
public bool ShouldRest { get; set; } = false;
// [Pode Mudar] Se você quer usar medidas anti lag: lag killer (tela preta), deixar monstros invisíveis e deixar o FPS limitado em 10
public bool AntiLag { get; set; } = true;
// [Pode Mudar] O intervalo, em milissegundos, que as skills serão usadas se estiverem disponíveis.
public int SkillTimer { get; set; } = 100;
// [Pode Mudar] Nome da sua classe de solo
public string SoloClass { get; set; } = "Generic";
// [Pode Mudar] (Use o Skills > Advanced) Sequência de skills da class de solo
public string SoloClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Pode Mudar] (Use o Skills > Advanced) SkillTimeout da classe de solo
public int SoloClassSkillTimeout { get; set; } = 150;
// [Pode Mudar] Nome da sua classe de farm
public string FarmClass { get; set; } = "Generic";
// [Pode Mudar] (Use o Skills > Advanced) Sequência de skills da classe de farm
public string FarmClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Pode Mudar] (Use o Skills > Advanced) SkillTimeout da classe de farm
public int FarmClassSkillTimeout { get; set; } = 1;
// [Pode Mudar] Algumas Sagas utilizam a aliança do seu personagem para receber pontos de rep extra, mude a sua preferência (Alignment.Evil ou Alignment.Good).
public int HeroAlignment { get; set; } = (int)Alignment.Evil;
```

## Core Skill Plugin

> **Nas versões a partir do RBot 3.6.2, Core Skills foram implementadas como Advanced skills então você não precisa utilizar mais o plugin (clique em Skills > Advanced).**

Se você quer utilizar sequências de skill específicas para sua classe de solo ou farm, esse plugin vai the ajudar a criar a sequência que você quiser. Abaixo eu te guiarei em como criar uma sequência para a Void HighLord.

Primeiro o plugin precisa ser carregado, para isso você pode copiar o plugin para a pasta **"_Rbot/plugins/_"**, assim ele será carregado automaticamente quando você iniciar o RBot, ou você pode abrir a aba **"Plugins"** no RBot, clicar em _"Load"_ e procurar a pasta onde você extraiu o plugin :

<p align="center"><img src="https://imgur.com/IEVOrkl.png"></p>

Depois disso um botão vai aparecer no menu principal do RBot, clique em _"Core Skill"_ e você verá está interface:

<p align="center"><img src="https://imgur.com/AUIOhFe.png"></p>

1. Essa é a lista onde as skills serão adicionadas. Depois de adicioná-las você poderá utilizar:
   - Setas (Cima/Baixo) para navegar entre as skills;
   - Ctrl + Setas para mudar a ordem das skills;
   - Delete para remover a skill selecionada da lista;
   - Ctrl + Delete para remover todas as skills;
   - Você também pode arrastar as skills com o mouse ou clicar com o botão direito para fazer qualquer um dos comandos acima.
2. Aqui você adicionara regras para quando o bot deve usar a skill. Depois de mudar os valores você pode usar o botão direito do mouse e depois clicar em _"Reset"_ para voltar os valores padrão. Cada linha faz, respectivamente:
   - Checkbox que determina se você quer usar regras na skill;
   - A porcentagem de vida necessária para usar a skill, você pode clicar no sinal '>' para inverte-lo;
   - A quantidade de mana necessária para usar a skill, você pode clicar no sinal '>' para inverte-lo;
   - Quanto tempo, em milissegundos, que o bot irá esperar para usar a skill;
   - Checkbox que determina se a skill vai ser pulada se não estiver disponível.
3. Irá adicionar a skill (1-4) com as regras (caso você tenha checado a Checkbox) para a lista.
4. O modo de uso das skills:
   - Optimistic - Se o bot consegue usar a skill ele a usará. Senão ele irá pular para a próxima skill.
   - Wait (Padrão) - Irá esperar a skill estar disponível (ou o tempo acabar (SkillTimeout)) antes de utilizar a skill;
5. Uma calculadora para a propriedade [SkillTimeout](#customizing-corebots). Você preenche o valor de SkillTimer (padrão é 100) e o maior cooldown da sua classe, depois de apertar Enter você pode usar o valor ao lado de _"SkillTimeout"_ no arquivo **CoreBots.cs**;
6. Este botão irá converter as informações da lista para uma string que pode ser usada em **CoreBots.cs**, na propriedade _"ClassSkill"_. Depois de apertar este botão a string será copiada automaticamente para sua área de transferência.
7. Onde a string será mostrada apôs apertar o botão Convert.

A sequência de skills da Void Highlord para dar a maior quantidade de dano é `4-5-3-2-4-3-2`, para criar esta sequência no plugin primeiro temos que considerar que o número da skill é o número dela no jogo menos 1 (um) já que não contamos o auto ataque, então a sequência de skills será `3-4-2-1-3-2-1`. Primeiro adicionaremos a skill 3 _Unshackled_ que usa 20% da vida máxima, então para não morrermos à toa configuramos a regra assim:

<p align="center"><img src="https://imgur.com/X4bDDxG.png"></p>

E depois clicamos no botão **Add**. Note que eu deixei a regra de vida em 25% para nos dar uma margem de vida e chequei _"Skip if not available"_ para não ficarmos esperando a skill ficar disponível enquanto estamos com menos de 25% de vida. Seguindo este mesmo passo podemos concluir a sequência:

<p align="center"><img src="https://imgur.com/QNOASl5.png"></p>

O **Use Mode** é opcional mas recomendo que sempre utilize o modo de espera (salvo em classes que não necessitam de regras para uso). Com tudo pronto podemos apertar o botão **"Convert"**.

<p align="center"><img src="https://imgur.com/AKGlJY8.png"></p>

Para calcular o **SkillTimeout** (apenas calcule se estiver usando o **Use Mode Wait**) da VHL podemos usar a skill com maior cooldown que é  _(4) Armageddon_ com 15 segundos, converter para milissegundos (15000) e se o nosso **SkillTimer** é 100ms, o resultado será **SkillTimeout = 150**. Com todas as informações podemos fazer as respectivas mudanças no arquivo **CoreBots.cs** para nossa classe de solo:

```csharp
// [Pode Mudar] Nome da sua classe de solo
public string SoloClass { get; set; } = "Void Highlord";
// [Pode Mudar] (Use o Core Skill plugin) Sequência de skills da class de solo
public string SoloClassSkills { get; set; } = "3 H>25S | 4 | 2 | 1 H>25S | 3 H>25S | 2 | 1 H>25S";
// [Pode Mudar] (Use o Core Skill plugin) SkillTimeout da classe de solo
public int SoloClassSkillTimeout { get; set; } = 150;
```

Agora seus bots irão utilizar a classe e skills definidas quando necessário.

> **Notas:**
>
> - Você pode utilizar todas as regras de uso na mesma skill sem problema algum.
> - Regras do tipo "Wait" tem prioridade sobre todas as outras regras, mesmo se a skill não estiver disponível e "skip" estiver checado, o bot primeiro irá esperar o tempo definido e depois verificar as outras regras.

## FAQ

**Q:** Como baixar os scripts?  
**A:**

- Vá para a aba de [Releases](https://github.com/BrenoHenrike/Scripts/releases/tag/Scripts);
- Na postagem marcada com "Latest", baixe a pasta **Scripts.zip** abaixo de _Assets_;
- Coloque a pasta **Scripts.zip** dentro da pasta _Scripts do seu RBot_;
- Clique com o botão direito sobre **Scripts.zip** e depois clique em **"_Extrair aqui_"**;
- Caso apareça uma mensagem perguntando se você quer substituir os arquivos, clique em **_Sim_**;
- Agora é só utilizar os scripts!

> **Nota:** A versão mais recente do RBot você pode [baixar aqui](https://github.com/BrenoHenrike/RBot/releases).

**Q:** Quando tento usar CoreBots/Dailys/Farms da erro, o que eu faço?  
**A:** Arquivos que começam com **"*Core*"** não são bots, são apenas arquivos que os bots usam.

**Q:** Quando uso um bot o erro *"The type or namespace 'CoreBots' could not be found"* aparece, como consertar?  
**A:** Pode ser um problema na instalação, certifique-se que a pasta **"*Scripts*"** foi colada na mesma pasta do **"*RBot.exe*"**, desta maneira todos bots meus serão atualizados. Um erro comum de diretório é: *"\*/Rbot/Scripts/**Scripts**/FarmAllDailys.cs"* ou até *"\*/Rbot/**Scripts-master/Scripts**/FarmAllDailys.cs"* quando deveria ser *"\*/Rbot/**Scripts**/FarmAllDailys.cs"*.
> **Nota:** Se mesmo após seguir esta resposta e não der certo, abra o script que deu erro e verifique se as primeiras linhas que começam com `//cs_include` tem os nomes/diretórios corretos, as vezes eu posso escrever errado e não percerber.

**Q:** Mesmo depois de seguir as soluções meu script não funciona, o que eu faço agora?  
**A:** Então é bem possível de eu ter errado algo. Neste caso você pode me mandar uma mensangem no Discord: **Breno_Henrike#6959** e me dizer qual script está com problema.
