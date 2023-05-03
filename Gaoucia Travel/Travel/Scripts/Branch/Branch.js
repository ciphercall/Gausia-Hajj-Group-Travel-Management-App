var BranchApp = angular.module("BranchApp", ['ui.bootstrap']);

BranchApp.controller("ApiBranchController", function ($scope, $http) {


    var compid = "1";//for nothing
  

    $http.get('/apiBranch/GetAllData/', {
        params: {
            CompID: compid
          
        }
    }).success(function (data) {
      
        document.getElementById('txtcompid').focus();
     
        $scope.branchdata = data;

        $scope.loading = false;

    });



    $scope.add = function (event) {
        $scope.loading = false;

        event.preventDefault();

        var compid = $("#txtcompid").val();
        var branchname = $("#BranchName").val();
        var address = $("#Address").val();
        var contactno = $("#Contactno").val();
        var email = $("#Email").val();
        var status = $("#status").val();
        var inUserID = $('#InUserID').val();
        var insltude = $('#latlonPos').val();

        $http.get('/api/ApiBranch/GetData/', {
            params: {
                CompID: compid,
                BranchNm: branchname,
                Address: address,
                Contactno: contactno,
                Email: email,
                Status: status,
                InUserID: inUserID,
                InsLtude: insltude
            }
        }).success(function (data) {
            $("#txtcompid").val("");
            $("#BranchName").val("");
            $("#Address").val("");
            $("#Contactno").val("");
            $("#Email").val("");
            $("#status").val("");
            $('#InUserID').val("");
            $('#latlonPos').val("");
            document.getElementById('txtcompid').focus();
            alert("Saved");
            $scope.branchdata = data;

            $scope.loading = false;
            
        });
    };

    $scope.toggleEdit = function () {
        this.item.editMode = !this.item.editMode;

    };

    $scope.cancel = function () {
        this.item.editMode = !this.item.editMode;
        var compid = "1";

       

        $http.get('/apiBranch/GetAllData/', {
            params: {
                CompID: compid,

            }
        }).success(function (data) {

            document.getElementById('txtcompid').focus();

            $scope.branchdata = data;

            $scope.loading = false;

        });


    };

    $scope.save = function () {
        // alert("Edit");
        $scope.loading = true;
        var frien = this.item;
        this.item.COMPID = $('#txtcompid').val();
        this.item.STATUS = $('#gridstatus').val();


        $http.post('/api/ApiBranch/SaveData', this.item).success(function (data) {
            if (data.BRANCHID != 0) {
                alert("Saved Successfully!!");
            } else {
                alert("Duplicate data entered");
            }

            frien.editMode = false;

            var compid = "1";


            $http.get('/apiBranch/GetAllData/', {
                params: {
                    CompID: compid,

                }
            }).success(function (data) {

                document.getElementById('txtcompid').focus();

                $scope.branchdata = data;

               

            });

          

            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };

    $scope.deleteitem = function () {
        $scope.loading = true;
        var id = this.item.COMPID;
        var branchid = this.item.BRANCHID;
        $http.post('/api/ApiBranch/DeleteData/', this.item).success(function (data) {

            $.each($scope.branchdata, function (i) {
                if ($scope.branchdata[i].COMPID === id && $scope.branchdata[i].BRANCHID === branchid) {
                    $scope.branchdata.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };
   

});

BranchApp.directive('ngConfirmClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var msg = attr.ngConfirmClick || "Are you sure?";
                    var clickAction = attr.confirmedClick;
                    element.bind('click', function (event) {
                        if (window.confirm(msg)) {
                            scope.$eval(clickAction);
                        }
                    });
                }
            };
        }])