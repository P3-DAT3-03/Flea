const vertical = "Vertical";
const horizontal = "Horizontal";

function DefaultCluster(offsets) {
    return [
        // upper half
        [0, 0, vertical], [1, 0, horizontal],
        [3, 0, horizontal], [5, 0, vertical],

        // lower half
        [0, 3, vertical], [1, 4, horizontal],
        [3, 4, horizontal], [5, 3, vertical],
    ].map(tableInfo => ({
        X: tableInfo[0] + offsets.X,
        Y: tableInfo[1] + offsets.Y,
        Class: tableInfo[2]
    }));
}

const Posititioner = (startPosition, width, height, rowLength) => index => ({
    X: startPosition.X + (index % rowLength) * width,
    Y: startPosition.Y + Math.floor(index / rowLength) * height,
});

const clusters = (() => {
    let p = Posititioner({X: 0, Y: 0}, 7, 6, 4);
    return Object.fromEntries(
        ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L"]
            .map((key, i) => [key, DefaultCluster(p(i))])
    );
})();

console.log(JSON.stringify(clusters));