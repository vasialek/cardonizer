class AppViewModel
{
    constructor() {
        this._applicationDiv = document.getElementById("Application");
    }

    loadApp1() {
        console.log("1111");
        ko.cleanNode(this._applicationDiv);
        ko.applyBindings(new EldritchHorrorApp(), this._applicationDiv);
    }

    loadApp2() {
        console.log("2222");
    }
}

// ko.components.register("eldritch-horror-app", {
//     viewModel: { instance: new EldritchHorrorApp() },
//     template: { element: "EldritchHorrorAppTemplate" }
// });

// ko.components.register("runebound-app", {
//     viewModel: { instance: new RuneboundApp() },
//     template: { element: "RuneboundAppTemplate" }
// });

ko.applyBindings(new RuneboundApp(), document.getElementById("RuneboundAppTemplate"));
// ko.applyBindings(new AppViewModel());
