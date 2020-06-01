(function () {
    "use strict";
    angular.module("umbraco").controller("activeis.umbFormsCustomEmailController", function (
        $scope,
        editorService,
        activeisCustomEmailPickerResource) {

        $scope.openTreePicker = function () {

            var treePickerOverlay = {
                treeAlias: "activeisCustomEmailTemplates",
                section: "forms",
                entityType: "customemail-template",
                multiPicker: false,
                onlyInitialized: false,
                select: function (node) {
                    activeisCustomEmailPickerResource.getVirtualPathForEmailTemplate(node.id).then(function (response) {
                        //Set the picked template file path as the setting value
                        $scope.setting.value = response.data.path;
                    });

                    editorService.close();
                },
                close: function (model) {
                    editorService.close();
                }
            };

            editorService.treePicker(treePickerOverlay);

        };
    });
})();
