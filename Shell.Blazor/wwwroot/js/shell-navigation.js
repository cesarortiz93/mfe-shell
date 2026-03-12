// wwwroot/js/shell-navigation.js
window.listenMfeNavigation = function (dotnetRef) {
    window.addEventListener('message', function (event) {
        if (event.data && event.data.type === 'mfe-navigation') {
            dotnetRef.invokeMethodAsync('OnMfeNavigated', event.data.path);
        }
    });
};