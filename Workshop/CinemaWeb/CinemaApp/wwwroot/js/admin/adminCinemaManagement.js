async function confirmToggleDelete(cinemaId, actionType) {
  const form = document.getElementById(`toggleDeleteForm-${cinemaId}`);
  const isDelete = actionType === "delete";

  try {
    const result = await Swal.fire({
      title: isDelete ? "Are you sure?" : "Restore Cinema?",
      text: isDelete ? "Do you really want to delete this cinema?" : "This will restore the cinema and make it active again.",
      icon: isDelete ? "warning" : "question",
      showCancelButton: true,
      confirmButtonColor: isDelete ? "#d33" : "#3085d6",
      cancelButtonColor: "#6c757d",
      confirmButtonText: isDelete ? "Yes, delete it!" : "Yes, restore it!",
      cancelButtonText: "Cancel",
      reverseButtons: true,
    });

    if (!result.isConfirmed) {
      await Swal.fire({
        title: "Cancelled",
        text: isDelete ? "The cinema was not deleted." : "The cinema remains deleted.",
        icon: "info",
        timer: 1500,
        showConfirmButton: false,
      });
      return;
    }

    await Swal.close();

    Swal.fire({
      title: isDelete ? "Deleting..." : "Restoring...",
      text: "Please wait a moment.",
      icon: "info",
      showConfirmButton: false,
      allowOutsideClick: false,
      allowEscapeKey: false,
      didOpen: () => Swal.showLoading(),
    });

    setTimeout(async () => {
      await Swal.close();
      form.submit();
    }, 500);
  } catch (error) {
    console.error("Error during confirmation:", error);
    Swal.fire({
      title: "Error",
      text: "Something went wrong. Please try again.",
      icon: "error",
    });
  }
}
