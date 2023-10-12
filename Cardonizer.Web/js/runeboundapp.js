class RuneboundApp {
    constructor() {
        console.log("RuneboundApp initializing...");
        this.cardTypes = [
            new CardType("7cc5942d38c14c57820226f7014279a1", "Quest card (green)"),
            new CardType("b31d0712777345cbb779f4e6fece492e", "Action card (purple)"),
            new CardType("0194c57298e94819948951a48badd0f3", "Fight card (orange)"),
        ];

        this.questCard = null;
        this.actionCard = null;
        this.fightCard = null;
    }

    cardTypes() {
        return this.cardTypes;
    }

    onNextCardClicked(e) {
        console.log("onNextCardClicked");
        console.log(e);
    }

    _loadNextCard(cardTypeId) {

    }


}

class CardType {
    constructor(cardTypeId, name) {
        this.cardTypeId = cardTypeId;
        this.name = name;
    }

    isQuestCard() {
        return this.cardTypeId == "7cc5942d38c14c57820226f7014279a1";
    }

    isActionCard() {
        return this.cardTypeId == "b31d0712777345cbb779f4e6fece492e";
    }

    isFightCard() {
        return this.cardTypeId == "0194c57298e94819948951a48badd0f3";
    }
}


class RuneboundQuestCard {
    constructor() {
        // this.cardId = "3";
        // this.cardTypeId = "7cc5942d38c14c57820226f7014279a1";
        this.title = "Просьба графа";
        this.description = "Граф замка Сандергард попросил...";
        this.questTask = "Исследуйте";
        this.possibleRewards = [];
        this.payload = null;
    }
}
