function ER(baseUrl) {
    this.baseUrl = baseUrl;
    this.subjectList = [];

    var _this = this;
    $.ajax({
        url: this.baseUrl + "_Subjects",
        type: "GET",
        dataType: "json",
        success: function (res) {
            console.log('subjects loaded!');
            console.log(res);
            _this.subjectList = res;
        },
        error: function (err) {
            console.log(err);
            alert("Error loading subjects!");
        }
    });
}

ER.prototype.searchSubjects = function (searchArg, limit) {

    let search = searchArg.trim().toLowerCase();
    let res = [];
    let i = 0;

    if (search && search.length > 0) {

        while (res.length <= limit && i < this.subjectList.length) {
            if (this.subjectList[i].id.startsWith(search))
                res.push(this.subjectList[i]);
            i++;
        }

        i = 0;

        while (res.length <= limit && i < this.subjectList.length) {
            if (this.subjectList[i].name.toLowerCase().includes(search))
                res.push(this.subjectList[i]);
            i++;
        }
    }

    return res;
};

ER.prototype.treeItemClick = function (id, level) {

    const $div = $(".tree-item-children[data-id='" + id + "']");

    if (level < 4) {
        $div.slideToggle();
    }
    else if ($div.is(":visible")) {
        $div.slideUp();
    }
    else {
        this.showAccounts(id);
    }
};

ER.prototype.showAccounts = function (functionId) {

    this.functionId = functionId;

    const $div = $(".tree-item-children[data-id='" + functionId + "']");

    $.ajax({
        url: this.baseUrl + "_TreeAccounts/" + functionId,
        type: "GET",
        success: function (res) {
            $div.slideUp().html(res).slideDown();
        },
        error: function () {
            alert("Error loading accounts!");
        }
    });
};

ER.prototype.create = function (functionId, div) {
    console.log("create account, function = " + functionId);
    $.ajax({
        url: this.baseUrl + "_Create/" + functionId,
        type: "GET",
        success: function (res) {
            div.slideUp().html(res).slideDown();
        },
        error: function () {
            alert("Error loading form!");
        }
    });
};

ER.prototype.edit = function (accountId, div) {
    console.log("edit account, id = " + accountId);
    $.ajax({
        url: this.baseUrl + "_Edit/" + accountId,
        type: "GET",
        success: function (res) {
            div.slideUp().html(res).slideDown();
        },
        error: function () {
            alert("Error loading form!");
        }
    });
};

ER.prototype.cancel = function () {
    this.showAccounts(this.functionId);
};

ER.prototype.submit = function (form, isNew, oldId) {
    console.log("submit form");
    console.log("isNew=" + isNew + " oldId=" + oldId);
    const data = form.serialize();
    console.log(data);
    $.ajax({
        url: this.baseUrl + "_Submit?isNew=" + isNew + "&oldId=" + oldId,
        type: "POST",
        data: data,
        success: function (res) {
            form.parent().html(res);
        },
        error: function () {
            alert("Error submitting form!");
        }
    });
};

ER.prototype.delete = function (accountId) {
    console.log("delete account, id = " + accountId);

};
