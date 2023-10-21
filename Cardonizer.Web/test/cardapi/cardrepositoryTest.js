const CardRepository = require("../../js/cardapi/cardrepository");
require("assert");

const repository = new CardRepository("http://localhost:5099/");

describe("Get cards", function() {
    it("Should returns next card", function() {
        repository.getNextCard();
    });

    it("Should get cards using same game session", async () => {
        await repository.getNextCard();
        await repository.getNextCard();
        await repository.getNextCard();
    });
});
