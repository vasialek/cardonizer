const fetch = require('node-fetch');

class CardRepository {
    _gameNameId = "f4a48dd9f4604033a2a7f76d04e79db4";

    constructor(baseUrl) {
        console.log("ctor...");
        this._baseUrl = baseUrl;
        this._gameSession = null;
    }

    async getNextCard() {
        const response = await this._getGameSession(this._gameNameId);
        // console.log("API response:", response);

        const gameSessionId = response.gameSession.gameSessionId;
        return await this._fetchNextCard(gameSessionId, "7cc5942d38c14c57820226f7014279a1");
    }

    async _fetchNextCard(gameSessionId, cardType) {
        const url = `${this._baseUrl}api/card/getnextcard?cardTypeId=${cardType}&gameSessionId=${gameSessionId}`;
        console.log("Fetching next card:", url);
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Network response was not OK');
        }

        const card = await response.json();
        console.log("Next card response:", card.cardId);
        return card;

    }

    async _getGameSession(gameNameId) {
        console.log("Game session:", this._gameSession);
        if (this._gameSession) {
            return this._gameSession;
        }

        console.log("Calling start game...");
        const response = await fetch(this._baseUrl + "api/game/startgame?gameNameId=" + gameNameId, {method: "POST"});
        if (!response.ok) {
            throw new Error('Network response was not OK');
        }

        const gameSession = await response.json();
        this._gameSession = gameSession;
        console.log("Game session stored:", this._gameSession);
        return gameSession;
    }
}


module.exports = CardRepository;
