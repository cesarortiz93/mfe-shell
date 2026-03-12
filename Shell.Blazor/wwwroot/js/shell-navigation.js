// wwwroot/js/shell-navigation.js
window.listenMfeNavigation = function (dotnetRef) {
    window.addEventListener('message', function (event) {
        console.log('Shell recibió mensaje:', event.data);
        if (event.data && event.data.type === 'mfe-navigation') {
            console.log('Navegando MFE a:', event.data.path);
            dotnetRef.invokeMethodAsync('OnMfeNavigated', event.data.path);
        }
    });
};