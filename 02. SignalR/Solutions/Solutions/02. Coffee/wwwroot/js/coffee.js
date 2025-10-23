"use strict";

"use strict";

let connection = null;

const setupConnection = async () => {
  if (connection) {
    try {
      await connection.stop();
    } catch (err) {
      console.warn("Error stopping connection:", err);
    }
  }

  connection = new signalR.HubConnectionBuilder().withUrl(`/coffeehub`).build();

  connection.on(`ReceiveOrderUpdate`, (update) => {
    document.getElementById(`status`).innerHTML = update;
  });

  connection.on(`NewOrder`, (order) => {
    document.getElementById(`status`).innerHTML =
      `Someone ordered a ` + order.product;
  });

  connection.on(`Finished`, async () => {
    await connection.stop();
    console.log("Connection closed after order finished.");
  });

  await connection.start();
  console.log("Connection started!");
};

document.getElementById(`submit`).addEventListener(`click`, async (e) => {
  e.preventDefault();

  await setupConnection();

  const product = document.getElementById(`product`).value;
  const size = document.getElementById(`size`).value;

  const response = await fetch(`/Coffee/OrderCoffee`, {
    method: `POST`,
    body: JSON.stringify({ product, size }),
    headers: {
      "content-type": `application/json`,
    },
  });

  const orderId = await response.text();

  // ✅ now that connection is started, you can safely invoke
  await connection.invoke(`GetUpdateForOrderAsync`, +orderId);
});
