let sidebar = document.querySelector(".sidebar");
let sidebarBtn = document.querySelector(".sidebarBtn");
let logoName = document.querySelector(".logo_name");
let linkNames = document.querySelectorAll(".links_name");

sidebarBtn.onclick = function() {
  sidebar.classList.toggle("active");

  if(sidebar.classList.contains("active")) 
  {
    sidebarBtn.classList.replace("bx-menu" ,"bx-menu-alt-right");
    logoName.classList.add("d-none");
    linkNames.forEach((linkName) => {
      linkName.classList.add("d-none");
    });
  }
  else
  {
    sidebarBtn.classList.replace("bx-menu-alt-right", "bx-menu");
    logoName.classList.remove("d-none");
    linkNames.forEach((linkName) => {
      linkName.classList.remove("d-none");
    });
  }
}
