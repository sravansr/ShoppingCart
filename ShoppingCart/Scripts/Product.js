angular.module("ProductApp", [])

    .controller("ProductController", function ($scope) {
        $scope.isDisplyProductdetails = false;
       

        $scope.GetAndDisplayProductDetails = function (productId) {

            $.ajax({
                url: urlGetProductDetailsById,
                type: 'GET',
                data: { ProductId: productId },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    //hide and showing the edit form
                    $scope.isDisplyProductdetails = true;
                    //assigning the values to edit form fields
                    $scope.ProductName = result.ProductName;
                    $scope.ProductQuality = result.ProductQuality;
                    $scope.Price = result.Price;
                    $scope.Description = result.ProductDescription;
                    $scope.ProductID = result.ProductID;
                    $scope.$apply();//force fully binding the data
                },
                error: function (error) {
                    alert(error)
                }
            });
        }

        $scope.DeleteProductDetailsByID = function (productId) {

            $.ajax({
                url: urlDeleteProductDetailsById,
                type: 'GET',
                data: { ProductId: productId },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    $scope.GetProductDetails()
                    alert(result);
                },
                error: function (error) {
                    alert(error)
                }
            });
        }

        $scope.UpdateProductDetails = function()
        {
            $.ajax({
                url: urlUpdateProductDetailsById,
                type: 'GET',
                data: {
                    ProductId: $scope.ProductID,
                    ProductName: $scope.ProductName,
                    ProductQuality: $scope.ProductQuality,
                    Price: $scope.Price,
                    Description: $scope.Description
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    //hide and showing the edit form
                    $scope.isDisplyProductdetails = false;
                    alert(result);
                    $scope.GetProductDetails();

                    //$scope.$apply();//force fully binding the data
                },
                error: function (error) {
                    alert(error)
                }
            });
        }
        $scope.GetProductDetails=   function () {
            $.ajax({
                url: urlGetProductDetails,
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    $scope.Products = result;
                    $scope.$apply();

                },
                error: function (error) {
                    alert(error)
                }
            });
        }
        $scope.GetProductDetails()
    });