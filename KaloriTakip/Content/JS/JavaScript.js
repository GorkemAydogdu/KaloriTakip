const btnShow = document.querySelector(".btnShow");
const password = document.querySelector(".password");

btnShow.addEventListener("mousedown", () => { /*mouse down oldugunda şifrenin typi text*/
    password.setAttribute("type", "text");
})

btnShow.addEventListener("mouseup", () => { /*mouse up özelliği olduğunda şifrenin type password*/
    password.setAttribute("type", "password");
})

