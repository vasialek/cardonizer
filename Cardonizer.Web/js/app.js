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

ko.components.register("eldritch-horror-app", {
    viewModel: { instance: new EldritchHorrorApp() },
    template: { element: "EldritchHorrorAppTemplate" }
});

ko.applyBindings(new AppViewModel);
