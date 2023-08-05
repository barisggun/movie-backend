//const hamburger = document.querySelector(".hamburger-icon");
//const hamburgerMenu = document.querySelector(".hamburgerMenu");

//hamburger.addEventListener("click", () => {
//    if (hamburgerMenu.style.display === "block") {
//        hamburgerMenu.style.display = "none";
//    } else {
//        hamburgerMenu.style.display = "block";
//    }
//})
document.addEventListener("DOMContentLoaded", function () {
    const modal1 = document.getElementById("myModal1");
    const modal2 = document.getElementById("myModal2");

    const btn1 = document.querySelector('#modal1');
    const btn2 = document.querySelector('#modal2');

    const span1 = document.querySelector('#myModal1 .close');
    const span2 = document.querySelector('#myModal2 .close');

    // �zleme Listem modal pencerelerini a�an fonksiyon
    function openModal1() {
        modal1.style.display = "block";
    }

    // �zlediklerim modal pencerelerini a�an fonksiyon
    function openModal2() {
        modal2.style.display = "block";
    }

    btn1.onclick = openModal1; // �zleme Listem butonuna t�klamada modal1 a��lacak
    btn2.onclick = openModal2; // �zlediklerim butonuna t�klamada modal2 a��lacak

    span1.onclick = function () {
        modal1.style.display = "none";
    };

    span2.onclick = function () {
        modal2.style.display = "none";
    };

    window.onclick = function (event) {
        if (event.target == modal1) {
            modal1.style.display = "none";
        }
        if (event.target == modal2) {
            modal2.style.display = "none";
        }
    };
});

function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

function openNavFilter() {
    document.getElementById("mySidenavLeft").style.width = "250px";
}

function closeNavFilter() {
    document.getElementById("mySidenavLeft").style.width = "0";
}


