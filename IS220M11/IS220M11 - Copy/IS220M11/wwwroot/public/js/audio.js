function myPlay(a,b) {
    var playbutton = document.getElementById(a)
    var audio = document.getElementById(b)
    var status = playbutton.getAttribute("src");
    if ((status == "public\\assets\\img\\stop.png")||(status =="public/assets/img/stop.png")){
        playbutton.src='public/assets/img/play.png'
        audio.play();
        audio.loop();
    }
    
}
function myStop(a,b) {
    var playbutton = document.getElementById(a)
    var audio = document.getElementById(b)
    var status = playbutton.getAttribute("src");
    if (status == "public/assets/img/play.png"){
        playbutton.src='public/assets/img/stop.png'
        audio.pause();
        audio.loop();
    }
}

