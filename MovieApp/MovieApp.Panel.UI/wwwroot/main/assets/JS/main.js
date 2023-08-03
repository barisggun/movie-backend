const hamburger = document.querySelector(".hamburger-icon");
const hamburgerMenu = document.querySelector(".hamburgerMenu");

hamburger.addEventListener("click", () => {
    if (hamburgerMenu.style.display === "block") {
        hamburgerMenu.style.display = "none";
    } else {
        hamburgerMenu.style.display = "block";
    }
})

var modal1 = document.getElementById("myModal1");
var modal2 = document.getElementById("myModal2");

var btn1 = document.querySelector('#modal1');
var btn2 = document.querySelector('#modal2');

var span1 = document.querySelector('#modal1 .close');
var span2 = document.querySelector('#modal2 .close');

btn1.onclick = function () {
    modal1.style.display = "block";
}
btn2.onclick = function () {
    modal2.style.display = "block";
}

span1.onclick = function () {
    modal1.style.display = "none";
}
span2.onclick = function () {
    modal2.style.display = "none";
}

window.onclick = function (event) {
    if (event.target == modal1) {
        modal1.style.display = "none";
    }
    if (event.target == modal2) {
        modal2.style.display = "none";
    }
}
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}
