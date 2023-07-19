const Config = {
	baseApiUrl: process.env.API_URL ?? "https://localhost:8001/",
};

const currencyFormatter = Intl.NumberFormat("en-US", {
	style: "currency",
	currency: "USD",
	maximumFractionDigits: 0,
});
function isProdEnv() {
	return process.env.NODE_ENV !== "development";
}

export default Config;
export { currencyFormatter, isProdEnv };
