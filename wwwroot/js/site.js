document.querySelectorAll('#myNav .nav-link').forEach(function (item) {
  item.addEventListener('mouseenter', function () {
    document.querySelector('#myNav .active')?.classList.remove('active');
    this.classList.add('active');
  });
});
