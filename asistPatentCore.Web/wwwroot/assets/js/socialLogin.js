window.fbAsyncInit = function () {

            FB.init({
                appId: '276966980291317',
                cookie: true,
                xfbml: true,
                version: 'v7.0'
            });
            FB.AppEvents.logPageView();


        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "https://connect.facebook.net/tr_TR/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        //function checkLoginState() {
        //    FB.getLoginStatus(function (response) {
        //        //statusChangeCallback(response);
        //        if (response.status == 'connected')
                    
        //    });

        //}
        function fbLogin() {
            FB.login(function (response) {
                if (response.authResponse) {
                    // Get and display the user profile data
                    //getFbUserData();
                    
                    setLoginSocial(response.authResponse.accessToken, response.authResponse.userID);
                } else {
                    
                }
            }, { scope: 'email' });
}
function fbLogoutUser() {
    FB.getLoginStatus(function (response) {
        if (response && response.status === 'connected') {
            FB.logout(function (response) {
                
                document.getElementById('logoutform').submit();
            });
        }
    });
}
function onSignIn(googleUser) {

    var id_token = googleUser.getAuthResponse().id_token;
    setLoginSocial(id_token, "");

}

function setLoginSocial(token, userid) {

    $.ajax({
        url: '/signinSocial',
        type: 'POST',
        dataType: 'json',
        data: { token: token, userid: userid },
        success: function (data) {
            if (data == "Ok") {
                window.location.href = "/anasayfa";
            }
            else {

            }
        }
    });
}
function signOut() {
    gapi.load('auth2', function () {
        gapi.auth2.init().then(function () { 
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.signOut().then(function () {
                console.log("User is logged out");
                document.getElementById('logoutform').submit();
            });
        });
    });     
};
function signOutSocial(provider) {
    if (provider == "Facebook")
        fbLogoutUser();
    else if (provider == "Google")
        signOut();
    else
        document.getElementById('logoutform').submit();
   
}

