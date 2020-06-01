function activeisCustomEmailPickerResource($http) {

    var apiRoot = "backoffice/ActiveISCustomEmail/Picker/";

    return {
        getVirtualPathForEmailTemplate: function (encodedPath) {
            return $http.get(apiRoot + "GetVirtualPathForEmailTemplate?encodedPath=" + encodedPath);
        }

    };
}

angular.module('umbraco.resources').factory('activeisCustomEmailPickerResource', activeisCustomEmailPickerResource);