/// <reference path="../vanillaAjax.js" />
/// <reference path="../knockout-3.2.0.js" />

var ViewModel = function () {
	var self = this;

	self.Categories = ko.observableArray([]);
	self.NewCategoryName = ko.observable("");

	self.NewReturned = function (data) {
		console.log(data);
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

	ajax.get("/api/nforum/category", null, self.DataReturned);
};

var vm = new ViewModel();
ko.applyBindings(vm);
