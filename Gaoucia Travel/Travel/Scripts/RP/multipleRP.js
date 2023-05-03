var MultipleRP = angular.module("MultipleRP", ['ui.bootstrap']);

MultipleRP.controller("ApiMultipleRPController", function ($scope, $http) {

    var flag = 0;
    $scope.add = function (event) {
        $scope.loading = true;

        event.preventDefault();


        var compid = $('#txtcompid').val();


        var transdt = $('#TransactionDate').val();
        var transmy = $('#Transmonthyear').val();
        var transno = $('#TransactionNo').val();
        var cardcid = $('#Cardcid').val();
        var cardpid = $('#Cardpid').val();
        var transfor = $('#TransactionFor').val();
     

        var inUserID = $('#InUserID').val();
        var insltude = $('#latlonPos').val();



        $http.get('/api/ApiMultipleRP/GetData/', {
            params: {
                Compid: compid,

              
                Transfor: transfor,
                Transdt: transdt,
                Month: transmy,
                Tno: transno,
                Cardcid: cardcid,
                Cardpid:cardpid,

                InUserID: inUserID,
                InsLtude: insltude

            }
        }).success(function (data) {
            
            var remarks = data[0].Remarks;
            if (remarks == "holo na") {
                alert("Master Data Entry Wrong");
            }
            else if (remarks != "no data") {
                //alert("Continue");
                $scope.rpData = data;



            } else {
                //alert("Continue");
                $scope.rpData = null;

            }

            //document.getElementById('TransFor').focus();

            $scope.loading = false;

        });

    };

    $scope.addrow = function () {
        $scope.loading = false;
        event.preventDefault();


        $('#TicketDate').attr('readonly', false);

        this.newChild.CompanyId = $('#txtcompid').val();


        this.newChild.TransactionDate = $("#TransactionDate").val();
        this.newChild.Transmonthyear = $("#Transmonthyear").val();
        this.newChild.TransactionNo = $("#TransactionNo").val();

        this.newChild.Cardcid = $("#Cardcid").val();
        this.newChild.Cardpid = $("#Cardpid").val();


        this.newChild.TransactionFor = $('#TransactionFor').val();
        var changedtxt = $('#TransactionFor').val();
        if (changedtxt == "RECEIVABLE") {
            this.newChild.TicketDate = $("#TicketDate").val();
            this.newChild.Ticketno = $("#Ticketno").val();
        }
        else if (changedtxt == "PAYABLE") {
            this.newChild.TicketDate = $("#TicketDate2").val();
            this.newChild.Ticketno = $("#Ticketno2").val();
        }

        this.newChild.Cardno = $("#Cardno").val();
        this.newChild.Accountcd = $("#Accountcd").val();
        

    
      

        this.newChild.Amount = $("#Amount").val();
        this.newChild.Cashamount = $("#Cashamount").val();

        this.newChild.Creditamount = $("#Creditamount").val();
        this.newChild.Remarks = $("#Remarks").val();

        this.newChild.INSUSERID = $('#InUserID').val();
        this.newChild.INSLTUDE = $('#latlonPos').val();

        if (this.newChild.Cardno != "" && this.newChild.TransactionNo != "" && this.newChild.Accountcd != "" && this.newChild.Ticketno != "") {
            $http.post('/apiMultipleRP/ChildData', this.newChild).success(function (data, status, headers, config) {


                var compid = $('#txtcompid').val();


                var transdt = $('#TransactionDate').val();
                var transmy = $('#Transmonthyear').val();
                var transno = $('#TransactionNo').val();
                var cardcid = $('#Cardcid').val();
                var cardpid = $('#Cardpid').val();
                var transfor = $('#TransactionFor').val();


                var inUserID = $('#InUserID').val();
                var insltude = $('#latlonPos').val();



                $http.get('/api/ApiMultipleRP/GetData/', {
                    params: {
                        Compid: compid,


                        Transfor: transfor,
                        Transdt: transdt,
                        Month: transmy,
                        Tno: transno,
                        Cardcid: cardcid,
                        Cardpid: cardpid,

                        InUserID: inUserID,
                        InsLtude: insltude

                    }
                }).success(function (data) {

                    var remarks = data[0].Remarks;
                    if (remarks == "holo na") {
                        alert("Master Data Entry Wrong");
                    }
                    else if (remarks != "no data") {
                        //alert("Continue");
                        $scope.rpData = data;



                    } else {
                        //alert("Continue");
                        $scope.rpData = null;

                    }

                    //document.getElementById('TransFor').focus();

                    $scope.loading = false;

                });

                $("#Cardno").val("");

                $("#Accountcd").val("");
                $("#Ticketno").val("");

                $("#TicketDate").val("");
                $("#Amount").val("");

                $('#Cashamount').val("");
                $('#Creditamount').val("");
               
                $('#Remarks').val("");



            }).error(function () {
                $scope.error = "An Error has occured while loading posts!";
                $scope.loading = false;
            });

        } else {


            alert("Enter Master Data First");
        }

    };


    $scope.toggleEdit = function () {
        this.item.editMode = !this.item.editMode;

       
        var amount = $(".gridAmount").val();
        var cashamount = $(".gridCashamount").val();
        var creaditamount = $(".gridCreditamount").val();
        //$('#gridCashamount').change(function () {
        //    document.getElementById('gridCreditamount').value = (document.getElementById('gridAmount').value - (document.getElementById('gridCashamount').value));


        //});

        $('.gridCashamount').change(function () {
            $(".gridCreditamount").val(amount - cashamount);


        });
        $('.gridAmount').change(function () {
            //document.getElementById('gridCashamount').value = (document.getElementById('gridAmount').value);
            var temp=$('.gridAmount').val();
            $(".gridCashamount").val(temp);


        });

        var changedtxt = $('#TransactionFor').val();
        if (changedtxt == "RECEIVABLE") {
            $('#lableaccountnm').text('Receivable Head');

            $("#payablediv1").hide();
            $("#payablediv2").hide();
            $("#payablediv3").hide();
            $("#payablediv4").hide();
            $("#payablediv5").hide();
            $("#payablediv6").hide();

            $("#receivablediv1").show();
            $("#receivablediv2").show();
            $("#receivablediv3").show();
            $("#receivablediv4").show();
            $("#receivablediv5").show();
            $("#receivablediv6").show();
        }
        else if (changedtxt == "PAYABLE") {
            $('#lableaccountnm').text('Payable Head');

            $("#receivablediv1").hide();
            $("#receivablediv2").hide();
            $("#receivablediv3").hide();
            $("#receivablediv4").hide();
            $("#receivablediv5").hide();
            $("#receivablediv6").hide();

            $("#payablediv1").show();
            $("#payablediv2").show();
            $("#payablediv3").show();
            $("#payablediv4").show();
            $("#payablediv5").show();
            $("#payablediv6").show();
        }

        $('#gridTicket').change(function () {
            var ticket = $("#gridTicket").val();
            $.ajax({
                url: '/RPInfo/TicketDate',
                type: 'GET',
                cache: false,
                data: { query: ticket },
                dataType: 'json',
                success: function (data) {

                    $("#gridTicketDate").val(data);
                    $('#gridTicketDate').attr('readonly', true);

                }
            });
        });

        accountload();
    };

    $scope.x = function (value) {

        flag = 1;
        var compid = $('#txtcompid').val();

        $('.gridCardno').val(value);
        $('.gridCardno').autocomplete({
            source: function (request, response) {

                var params = {
                    type: 'GET',
                    cache: false,
                    data: { query: request.term, query2: compid },
                    dataType: 'json',
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.Cardno,
                                value: item.Cardno,
                                id: item.passenger
                            };
                        }));


                    }
                };


                params.url = '/apiMultipleRP/CardNo';


                $.ajax(params);


            },
            select: function (event, ui) {
                $('.gridCardno').val(ui.item.label);

                $('#gridPassenger').val(ui.item.id);


                accountload();
            },
            minLength: 1,



        });
    };
    function accountload() {
        var changedtxt = $('.gridCardno').val();
        var changedtxt2 = $('#TransactionFor').val();

        $.ajax
           ({
              
               //url: '@Url.Action("AccountLoad", "RPInfo")',
               url: '/RPInfo/AccountLoad',
               type: 'post',
               dataType: "json",
               data: { type: changedtxt, type2: changedtxt2 },
               success: function (data) {
                   $("#gridAccountcd").empty();

                   //if (changedtxt2 == "PAYABLE") {
                   //    $("#gridAccountcd").append('<option value="'
                   //  + '">'
                   //  + "--Select--" + '</option>');
                   //}




                   $.each(data, function (i, memo) {

                       $("#gridAccountcd").append('<option value="'
                           + memo.Value + '">'
                           + memo.Text + '</option>');




                   });

               }
           });
        if (changedtxt2 == "RECEIVABLE") {
            $.ajax
              ({
                  url: '/RPInfo/TicketLoad',
                  type: 'post',
                  dataType: "json",
                  data: { type: changedtxt, type2: changedtxt2 },
                  success: function (data) {
                      $("#gridTicket").empty();

                      $("#gridTicket").append('<option value="'
                        + '">'
                        + "--Select--" + '</option>');





                      $.each(data, function (i, memo) {

                          $("#gridTicket").append('<option value="'
                              + memo.Value + '">'
                              + memo.Text + '</option>');




                      });

                  }
              });
        }
     



    };

    $scope.cancel = function () {
        this.item.editMode = !this.item.editMode;

        var compid = $('#txtcompid').val();


        var transdt = $('#TransactionDate').val();
        var transmy = $('#Transmonthyear').val();
        var transno = $('#TransactionNo').val();
        var cardcid = $('#Cardcid').val();
        var cardpid = $('#Cardpid').val();
        var transfor = $('#TransactionFor').val();


        var inUserID = $('#InUserID').val();
        var insltude = $('#latlonPos').val();

        $http.get('/api/ApiMultipleRP/GetData/', {
            params: {
                Compid: compid,


                Transfor: transfor,
                Transdt: transdt,
                Month: transmy,
                Tno: transno,
                Cardcid: cardcid,
                Cardpid: cardpid,

                InUserID: inUserID,
                InsLtude: insltude

            }
        }).success(function (data) {

            var remarks = data[0].Remarks;
            if (remarks == "holo na") {
                alert("Master Data Entry Wrong");
            }
            else if (remarks != "no data") {
                //alert("Continue");
                $scope.rpData = data;



            } else {
                //alert("Continue");
                $scope.rpData = null;

            }

            //document.getElementById('TransFor').focus();

            $scope.loading = false;

        });

    


    };

    $scope.save = function () {
        // alert("Edit");
        $scope.loading = true;
        var frien = this.item;

        $('#gridTicketDate').attr('readonly', false);

        this.item.CompanyId = $('#txtcompid').val();


        this.item.TransactionDate = $("#TransactionDate").val();
        this.item.Transmonthyear = $("#Transmonthyear").val();
        this.item.TransactionNo = $("#TransactionNo").val();

        this.item.Cardcid = $("#Cardcid").val();
        this.item.Cardpid = $("#Cardpid").val();

        this.item.Cardno = $(".gridCardno").val();


        this.item.TransactionFor = $('#TransactionFor').val();


        this.item.UPDLTUDE = $('#latlonPos').val();
        this.item.UPDUSERID = $('#InUserID').val();


        $http.post('/api/ApiMultipleRP/SaveData', this.item).success(function (data) {

            alert("Saved Successfully!!");


            frien.editMode = false;

            var compid = $('#txtcompid').val();


            var transdt = $('#TransactionDate').val();
            var transmy = $('#Transmonthyear').val();
            var transno = $('#TransactionNo').val();
            var cardcid = $('#Cardcid').val();
            var cardpid = $('#Cardpid').val();
            var transfor = $('#TransactionFor').val();


            var inUserID = $('#InUserID').val();
            var insltude = $('#latlonPos').val();


            $http.get('/api/ApiMultipleRP/GetData/', {
                params: {
                    Compid: compid,


                    Transfor: transfor,
                    Transdt: transdt,
                    Month: transmy,
                    Tno: transno,
                    Cardcid: cardcid,
                    Cardpid: cardpid,

                    InUserID: inUserID,
                    InsLtude: insltude

                }
            }).success(function (data) {

                var remarks = data[0].Remarks;
                if (remarks == "holo na") {
                    alert("Master Data Entry Wrong");
                }
                else if (remarks != "no data") {
                    //alert("Continue");
                    $scope.rpData = data;



                } else {
                    //alert("Continue");
                    $scope.rpData = null;

                }

                //document.getElementById('TransFor').focus();

                //$scope.loading = false;

            });



            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };


    $scope.deleteitem = function () {
        $scope.loading = true;
        var id = this.item.Id;
        //var branchid = this.item.BRANCHID;
        $http.post('/api/ApiMultipleRP/DeleteData/', this.item).success(function (data) {

            $.each($scope.rpData, function (i) {
                if ($scope.rpData[i].Id === id) {
                    $scope.rpData.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };


    $scope.GetSummOfAmount = function (rpData) {
        var summ = 0;
       
        for (var i in rpData) {
            summ = summ + Number(rpData[i].Amount);
        }
        
        $('#gridTotalamount').val(summ);
       


        return summ;
    };
    $scope.GetSummOfcash = function (rpData) {
        var summ = 0;

        for (var i in rpData) {
            summ = summ + Number(rpData[i].Cashamount);
        }

        $('#gridCashamount').val(summ);



        return summ;
    };
    $scope.GetSummOfcredit = function (rpData) {
        var summ = 0;

        for (var i in rpData) {
            summ = summ + Number(rpData[i].Creditamount);
        }

        $('#gridCreditamount').val(summ);



        return summ;
    };

});