

function getUrl(url) {
    let ret = $("#hid_url").val() + "api/" + url;
    return ret;
}

function api_ret() {
    this.data = null;
    this.textStatus = null;
    this.jqXHR = null;
}

function doApiPost(url, e, parms, cb) {
    console.log("doApiPost", url);

    let parmsJson = JSON.stringify(parms);

    let _url = getUrl(url);

    var ret = new api_ret();

    $.ajax(
        {
            type: "POST",
            data: parmsJson,
            url: _url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        }).done(function (data, textStatus, jqXHR) {
            ret.data = data;
            ret.textStatus = textStatus;
            ret.jqXHR = jqXHR;
            cb(ret, e);
        }).fail(function (jqXHR, textStatus) {
            ret.textStatus = textStatus;
            ret.jqXHR = jqXHR;
            cb(ret, e);
        });

}


(() => {
    'use strict'

    var toastBeingUpdated = null;

    $("body").on('click', "#toast_beingupdated_close", function () {
        toastBeingUpdated.hide();
        gtag_set('ezeway_biz', 'toast_close');
    });

    $("body").on('click', ".btn_ga", function () {
        let obj_name = this.name;
        let cl = this.classList;

        gtag_set('ezeway_biz',obj_name);

        if (cl !== null && cl.contains("btn_toast_bypass") === false) { 
            toastBeingUpdated = $("#toast_beingupdated");
            //const toastInstance = bootstrap.Toast.getOrCreateInstance(toastBeingUpdated);
            toastBeingUpdated.show();
            //toastInstance.show();
        }
    });

    $("body").on('click', ".prd_img", function () {
        let pn = this.parentNode;
        let pn_data = $(pn).data();
        $("#mod_prd_imageurl").attr('src', pn_data.imageurl);
        $("#mod_prd_desc").html(pn_data.desc);
        $("#mod_prd_title").text(pn_data.title);
        $("#mod_prd_priceuk").text(pn_data.priceuk);
        $("#mod_prd_cta").attr('href', pn_data.producturl);
        $("#mod_prd_cta").attr('name', 'btn_prd_mod_' + pn_data.id);
        $("#mod_prd_cta").text(pn_data.cta);
        const modPrdImage = new bootstrap.Modal('#modalProduct', { });
        modPrdImage.show();
    });

    $("body").on('click', "#btn-login", function () {
        //const modLogin = new bootstrap.Modal('#modalSignin', { });
        //modLogin.show();
    });

    $("body").on('submit', '#frm_register', function (e) {
        e.preventDefault();
        var po = this;
        var p = {};
        if (po !== null) {
            for (var i = 0; i < po.length; i++) {
                switch (po[i].name) {
                    case "p_firstname":
                        p.cust_firstname = po[i].value.trim();
                        break;
                    case "p_lastname":
                        p.cust_lastname = po[i].value.trim();
                        break;
                    case "p_email":
                        p.cust_email = po[i].value.trim();
                        break;
                    case "p_mobile":
                        p.cust_mobile = po[i].value.trim();
                        break;
                    default:
                        break;
                }
            }
            p.cust_browserdetails = $("#hid_bd").val();

            if (p.cust_email === '' && p.cust_mobile === '') {
                alert("Please enter an email address or a mobile number");
            }
            else {
                doApiPost("customer/CustomerExistsEmail", e, p, function (ret, e) {
                    if (ret.data !== null && ret.data !== undefined && p.cust_email !== '' && ret.data.cust_email !== '') {
                        alert("This email already exists");
                    }
                    else {
                        doApiPost("customer/CustomerExistsMobile", e, p, function (ret, e) {
                            if (ret.data !== null && ret.data !== undefined && p.cust_mobile !== '' && ret.data.cust_mobile !== '') {
                                alert("This mobile number already exists");
                            }
                            else {
                                doApiPost("customer/CustomerSignupAvon", e, p, function (ret, e) {

                                });
                            }
                        });
                    }
                });
            }

            
        }
    });

})();

function doCreateCustomerContinue(ret, e) {
    var rd = ret.data;
    var ts = ret.textStatus;
}
