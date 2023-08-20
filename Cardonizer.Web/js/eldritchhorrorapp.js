class EldritchHorrorApp {
    constructor() {
        this.title = ko.observable("");
        this.description = ko.observable("");
        this.effect = ko.observable("");
        this.reckoning = ko.observable("");
    }

    onSaveClicked() {
        let xhr = new XMLHttpRequest();
        xhr.open("POST", "http://localhost:5099/api/cardadmin/addmytcard");
        xhr.send({
            
        });
    }
}
