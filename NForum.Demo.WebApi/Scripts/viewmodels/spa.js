/// <reference path="../vanillaAjax.js" />
/// <reference path="../knockout-3.2.0.js" />

var ViewModel = function () {
	var self = this;

	self.Categories = ko.observableArray([]);
	self.NewCategoryName = ko.observable("");

	self.NewReturned = function (data) {
		self.Categories.push(data);
		self.NewCategoryName("");
	};

	self.DataReturned = function (data) {
		ko.utils.arrayForEach(data, function (category) {
			self.Categories.push(category);
		});
	};

	self.Add = function (data, event) {
		ajax.post("/api/nforum/category", { "name": self.NewCategoryName(), sortOrder: 1 }, self.NewReturned);
	};

	self.Init = function() {
		ajax.get("/api/nforum/category/list", null, self.DataReturned);
	};

	self.Unauthorized = function (request) {
		console.log("ups");
	};
};

var vm = new ViewModel();

ajax.configure({ onUnauthorized: vm.Unauthorized });

ko.applyBindings(vm);
vm.Init();