/// <reference path="../vanillaAjax.js" />
/// <reference path="../knockout-3.2.0.js" />
/// <reference path="../knockout.mapping.js" />

var ViewModel = function () {
	var self = this;

	self.Categories = ko.observableArray([]);
	self.NewCategoryName = ko.observable("");

	self.NewUserName = ko.observable("");
	self.NewPassword = ko.observable("");
	self.NewEmailAddress = ko.observable("");

	self.CreateUser = function (data, event) {
		ajax.post("/api/account/register", { username: self.NewUserName(), email: self.NewEmailAddress(), password: self.NewPassword(), confirmPassword: self.NewPassword() }, self.Created);
	};

	self.Created = function (request) {
		ajax.post("/token", { grant_type: "password", username: self.NewUserName(), password: self.NewPassword() }, self.Success);
	};

	self.Success = function (request) {
		ajax.configure({ token: request.access_token });
	};

	self.NewReturned = function (data) {
		self.Categories.push(data);
		self.NewCategoryName("");
	};

	self.DataReturned = function (data) {
		ko.utils.arrayForEach(data, function (category) {
			self.Categories.push(new Category(category));
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

var Category = function (data) {
	var self = this;
	ko.mapping.fromJS(data, {}, self);

	self.Forums = ko.observableArray([]);

	self.AddForum = function (data, event) {
		console.log("w00t!");
	};
}

var Forum = function (data) {
	var self = this;
	ko.mapping.fromJS(data, {}, self);

	self.Topics = ko.observableArray([]);

	self.AddTopic = function (data, event) {
		console.log("w00t!");
	};
}

var vm = new ViewModel();

ajax.configure({ onUnauthorized: vm.Unauthorized });

ko.applyBindings(vm);
vm.Init();