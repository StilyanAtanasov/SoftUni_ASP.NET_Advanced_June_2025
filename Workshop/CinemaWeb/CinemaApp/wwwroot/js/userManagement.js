window.addEventListener(`DOMContentLoaded`, function () {
  document.querySelectorAll(`.btn-assign-role`).forEach((b) => {
    b.addEventListener(`click`, function (e) {
      console.log(e.target.closest(`.user-row`));
      const selectValue = e.target
        .closest(`.user-row`)
        .querySelector(`.role-select`).value;

      const userId = e.target.closest(`button`).dataset.userId;

      if (!selectValue || !userId) return;

      document.getElementById(`assignRoleInput-${userId}`).value = selectValue;
    });
  });

  document.querySelectorAll(`.btn-remove-role`).forEach((b) => {
    b.addEventListener(`click`, function (e) {
      console.log(e.target.closest(`.user-row`));
      const selectValue = e.target
        .closest(`.user-row`)
        .querySelector(`.role-select`).value;

      const userId = e.target.closest(`button`).dataset.userId;

      if (!selectValue || !userId) return;

      document.getElementById(`removeRoleInput-${userId}`).value = selectValue;
    });
  });
});
