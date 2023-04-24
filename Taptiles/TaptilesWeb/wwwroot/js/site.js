function createClickListeners() {
	$(".fruit").on('click', function () {
		const button = $(this);
		const isActive = button.hasClass('active');

		if (!isActive) {
			var row = button.data('row');
			var column = button.data('col');

			const fruit = {
				button,
				row,
				column
			};

			clickFruit(fruit);
		}
	});
}

async function deleteFruit(row1, column1, row2, column2) {
	const query = new URLSearchParams({
		row1, column1, row2, column2
	});
	const response = await fetch(`/Taptiles/Delete?${query.toString()}`, {
		method: 'Get',
	});
	const data = await response.text();

	const newTable = $("#game-table", data);
	const table = $("#game-table");

	table.replaceWith(newTable);
	createClickListeners();
}

let clickedFruits = [];

async function clickFruit(fruit) {
	clickedFruits.push(fruit);
	fruit.button.addClass('active');

	if (clickedFruits.length >= 2) {
		// const unclickedFruit = clickedFruits.shift();
	    // unclickedFruit.button.removeClass('active');
		console.log("string")

		const [first, second] = clickedFruits;
		await deleteFruit(first.row, first.column, second.row, second.column);
		clickedFruits.forEach(({ button }) => button.removeClass('active'));
		clickedFruits = [];
	}
}

$(document).ready(function () {
	createClickListeners();
});
