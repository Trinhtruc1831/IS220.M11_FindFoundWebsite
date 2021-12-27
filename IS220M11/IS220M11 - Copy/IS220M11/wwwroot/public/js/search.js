function mySearchMember() {
    var keyword = document.getElementById("keyword").value;
    var filter = document.getElementById("attribute");
    var attribute = filter.options[filter.selectedIndex].value;

    if (attribute == "") {
        document.getElementById("user").innerHTML = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("user").innerHTML = this.responseText;
        }
    };
        xmlhttp.open("GET","http://localhost:80/relaxchill/server/controller.php?action=searchMemberAjax&attribute="+attribute+"&keyword="+keyword,true);
        xmlhttp.send();
    }
}

//search admin manage music
function mySearchMusic() {
    var keyword = document.getElementById("keyword").value;
    var filter = document.getElementById("attribute");
    var attribute = filter.options[filter.selectedIndex].value;
    // alert(keyword, attribute);
    // exit();
    if (attribute == "") {
        document.getElementById("user").innerHTML = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("user").innerHTML = this.responseText;
        }
    };
        xmlhttp.open("GET","http://localhost:80/relaxchill/server/controller.php?action=searchMusicAjax&attribute="+attribute+"&keyword="+keyword,true);
        xmlhttp.send();
    }
}
//search admin manage history
function mySearchDiary() {
    var keyword = document.getElementById("keyword").value;
    var filter = document.getElementById("attribute");
    var attribute = filter.options[filter.selectedIndex].value;

    // alert(keyword, attribute);
    // exit();
    if (attribute == "") {
        document.getElementById("user").innerHTML = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("user").innerHTML = this.responseText;
        }
    };
        xmlhttp.open("GET","http://localhost:80/relaxchill/server/controller.php?action=searchDiaryAjax&attribute="+attribute+ "&keyword="+keyword,true);
        xmlhttp.send();
    }
}

