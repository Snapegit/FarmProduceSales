const config = {
    get() {
        return {
            url : process.env.VUE_APP_BASE_API_URL + process.env.VUE_APP_BASE_API + '/',
            name: process.env.VUE_APP_BASE_API,
			menuList:[
				{
					name: '商品信息管理',
					icon: 'icon-common12',
					child:[
						{
							name:'商品信息',
							url:'/index/shangpinxinxiList'
						},
					]
				},
				{
					name: '购物车管理',
					icon: 'icon-common2',
					child:[
						{
							name:'购物车',
							url:'/index/cartList'
						},
					]
				},
			]
        }
    },
    getProjectName(){
        return {
            projectName: "基于ASP.NET的助农销售系统"
        } 
    }
}
export default config
