const vertical = "Vertical";
const horizontal = "Horizontal";

function DefaultCluster(offsets) {
    return [
        [0, 0, vertical], [1, 0, horizontal],
        [3, 0, horizontal], [5, 0, vertical],
        [0, 3, vertical], [1, 4, horizontal],
        [3, 4, horizontal], [5, 3, vertical],
    ].map(tableInfo => ({
        X: tableInfo[0] + offsets.X,
        Y: tableInfo[1] + offsets.Y,
        Class: tableInfo[2]
    }));
}
