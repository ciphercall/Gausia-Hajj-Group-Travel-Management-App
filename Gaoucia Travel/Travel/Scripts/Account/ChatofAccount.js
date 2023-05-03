var AccountApp = angular.module("AccountApp", ['ui.bootstrap']);

AccountApp.controller("ApiAccountController", function ($scope, $http) {

    $scope.loading = false;
    $scope.addMode = false;


    $scope.search = function (event) {

        $scope.loading = true;
        event.preventDefault();

        var compid = $('#txtcompid').val();
        var type = $('#ddlHeadType option:selected').val();
        var accountCD = $('#hiddenText_txtAccountCD').val();

        $http.get('/api/ApiAccountController/GetAccountData/', {
            params: {
                Compid: compid,
                Type: type,
                AccountCD: accountCD,
            }
        }).success(function (data) {
            if (data[0].count == 1) {
                $scope.ChatOfAccountData = null;
            } else {
                $scope.ChatOfAccountData = data;
            }
            $scope.loading = false;
        });

    };




    $scope.toggleEdit = function () {
        var level = $('#txtLevelID').val();
        if (level != 4) {
            $('.gridBranceClass').prop("disabled", true);
            
        }
        this.testitem.editMode = !this.testitem.editMode;
    };



    $scope.toggleEdit_Cancel = function () {
        this.testitem.editMode = !this.testitem.editMode;

        //Grid view load 
        var compid = $('#txtcompid').val();
        var type = $('#ddlHeadType option:selected').val();
        var accountCD = $('#hiddenText_txtAccountCD').val();

        $http.get('/api/ApiAccountController/GetAccountData/', {
            params: {
                Compid: compid,
                Type: type,
                AccountCD: accountCD,
            }
        }).success(function (data) {
            $scope.ChatOfAccountData = data;
            $scope.loading = false;
        });
    };




    //Add grid level data
    $scope.addrow = function (event) {
        $scope.loading = false;
        event.preventDefault();

        var insert = $('#txt_Insertid').val();
        if (insert == "I") {
            $('#AccountNameID').val("");
            $('#AccountCdId').val("");
            $('#ControlCdId').val("");
            alert("Permission Denied!!");
        }
        else {
            this.newChild.COMPID = $('#txtcompid').val();
            this.newChild.INSUSERID = $('#txtInsertUserid').val();
            this.newChild.INSLTUDE = $('#latlon').val();

            this.newChild.HEADTP = $('#ddlHeadType option:selected').val();
            this.newChild.ACCOUNTCD = $('#hiddenText_txtAccountCD').val();
            this.newChild.ACCOUNTNM = $('#AccountNameID').val(); 
            this.newChild.BRANCHID = $('#branchNameID option:selected').val();
            this.newChild.HLEVELCD = $('#txtLevelID').val();
            //(this.newChild.ACCOUNTCD != "" || this.newChild.ACCOUNTCD != 0) &&
            if ( this.newChild.HEADTP != "" && this.newChild.ACCOUNTNM != "" && this.newChild.HLEVELCD != "") {
                $http.post('/api/ApiAccountController/grid/addData', this.newChild).success(function (data, status, headers, config) {

                    //Grid view load 
                    var compid = $('#txtcompid').val();
                    var type = $('#ddlHeadType option:selected').val();
                    var accountCD = $('#hiddenText_txtAccountCD').val();

                    $http.get('/api/ApiAccountController/GetAccountData/', {
                        params: {
                            Compid: compid,
                            Type: type,
                            AccountCD: accountCD,
                        }
                    }).success(function (data) {
                        $scope.ChatOfAccountData = data;
                        $scope.loading = false;
                    });


                    if (data.CONTROLCD != 0 && data.CONTROLCD != "") {
                        $('#AccountNameID').val("");
                        $('#AccountCdId').val("");
                        $('#ControlCdId').val("");
                        alert("Create Successfully !!");
                        //$scope.ChatOfAccountData.push({ ID: data.ID, MCATID: data.MCATID, MEDIID: data.MEDIID, MEDINM: data.MEDINM, PHARMAID: data.PHARMAID, GHEADID: data.GHEADID });
                    } else if (data.Limit == 0) {
                        $('#AccountNameID').val("");
                        $('#AccountCdId').val("");
                        $('#ControlCdId').val("");
                        alert("Entry not possible!");
                    } else {
                        $('#AccountNameID').val("");
                        $('#AccountCdId').val("");
                        $('#ControlCdId').val("");
                        alert("Duplicate name will not create!");
                    }

                }).error(function () {
                    $scope.error = "An Error has occured while loading posts!";
                    $scope.loading = false;
                });

            }
            else if (this.newChild.ACCOUNTNM == 0 || this.newChild.ACCOUNTNM == "") {
                $('#AccountNameID').val("");
                $('#AccountCdId').val("");
                $('#ControlCdId').val("");
                alert("Please input valid account name !!");
            }
            else {
                alert("Please input grid level data !!");
            }

        }
    };








    //Update to grid level data (save a record after edit)
    $scope.update = function () {
        $scope.loading = true;
        var frien = this.testitem;

        this.testitem.COMPID = $('#txtcompid').val();
        this.testitem.INSUSERID = $('#txtInsertUserid').val();
        this.testitem.INSLTUDE = $('#latlon').val();
       

        var Update = $('#txt_Updateid').val();
        if (Update == "I") {
            alert("Permission Denied!!");
            frien.editMode = false;

            //Grid view load 
            var compid = $('#txtcompid').val();
            var type = $('#ddlHeadType option:selected').val();
            var accountCD = $('#hiddenText_txtAccountCD').val();

            $http.get('/api/ApiAccountController/GetAccountData/', {
                params: {
                    Compid: compid,
                    Type: type,
                    AccountCD: accountCD,
                }
            }).success(function (data) {
                $scope.ChatOfAccountData = data;
            });

            $scope.loading = false;
        }
        else {
            $http.post('/api/ApiAccountController/grid/UpdateData', this.testitem).success(function (data) {
                if (data.CONTROLCD != 0) {
                    alert("Saved Successfully!!");
                } else {
                    alert("Duplicate data will not create!");
                }

                frien.editMode = false;

                //Grid view load 
                var compid = $('#txtcompid').val();
                var type = $('#ddlHeadType option:selected').val();
                var accountCD = $('#hiddenText_txtAccountCD').val();

                $http.get('/api/ApiAccountController/GetAccountData/', {
                    params: {
                        Compid: compid,
                        Type: type,
                        AccountCD: accountCD,
                    }
                }).success(function (data) {
                    $scope.ChatOfAccountData = data;
                });
                $scope.loading = false;

            }).error(function (data) {
                $scope.error = "An Error has occured while Saving Friend! " + data;
                $scope.loading = false;

            });
        }
    };






    //Delete grid level data.
    $scope.deleteItem = function () {
        $scope.loading = true;
        var id = this.testitem.ID;

        var Delete = $('#txt_Deleteid').val();
        if (Delete == "I") {
            alert("Permission Denied!!");
        }
        else {
            this.testitem.COMPID = $('#txtcompid').val();
            this.testitem.INSUSERID = $('#txtInsertUserid').val();
            this.testitem.INSLTUDE = $('#latlon').val();

            $http.post('/api/ApiAccountController/grid/DeleteData/', this.testitem).success(function (data) {
                if (data.ACCOUNTCD != 0)
                {
                    $.each($scope.ChatOfAccountData, function (i) {
                        if ($scope.ChatOfAccountData[i].ID === id) {
                            $scope.ChatOfAccountData.splice(i, 1);
                            return false;
                        }
                    });
                    $scope.loading = false;
                    alert("Delete Successfully!!");
                }
                else {
                    alert("Delete Child Data First!!");
                }
                

            }).error(function (data) {
                $scope.error = "An Error has occured while delete posts! " + data;
                $scope.loading = false;
            });
        }
    };



});