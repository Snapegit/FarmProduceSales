	import {
		createRouter,
		createWebHashHistory
	} from 'vue-router'
	import address from '@/views/address/list'
	import yonghu from '@/views/yonghu/list'
	import shangpinfenlei from '@/views/shangpinfenlei/list'
	import discussshangpinxinxi from '@/views/discussshangpinxinxi/list'
	import shangpinxinxi from '@/views/shangpinxinxi/list'
	import orders from '@/views/orders/list'
	import storeup from '@/views/storeup/list'
	import config from '@/views/config/list'
	import users from '@/views/users/list'
	import shangjia from '@/views/shangjia/list'
	import cart from '@/views/cart/list'
	import shangjiaRegister from '@/views/shangjia/register'
	import shangjiaCenter from '@/views/shangjia/center'

export const routes = [{
		path: '/login',
		name: 'login',
		component: () => import('../views/login.vue')
	},{
		path: '/',
		name: '首页',
		component: () => import('../views/index'),
		children: [{
			path: '/',
			name: '首页Home',
			component: () => import('../views/HomeView.vue'),
			meta: {
				affix: true
			}
		}, {
			path: '/updatepassword',
			name: '修改密码',
			component: () => import('../views/updatepassword.vue')
		}
		
		,{
			path: '/shangjiaCenter',
			name: '商家个人中心',
			component: shangjiaCenter
		}
		,{
			path: '/address',
			name: '地址',
			component: address
		}
		,{
			path: '/yonghu',
			name: '用户',
			component: yonghu
		}
		,{
			path: '/shangpinfenlei',
			name: '商品分类',
			component: shangpinfenlei
		}
		,{
			path: '/discussshangpinxinxi',
			name: '商品信息评论',
			component: discussshangpinxinxi
		}
		,{
			path: '/shangpinxinxi',
			name: '商品信息',
			component: shangpinxinxi
		}
		,{
			path: '/orders',
			name: '订单管理',
			component: orders
		}
		,{
			path: '/storeup',
			name: '我的收藏',
			component: storeup
		}
		,{
			path: '/config',
			name: '轮播图',
			component: config
		}
		,{
			path: '/users',
			name: '管理员',
			component: users
		}
		,{
			path: '/shangjia',
			name: '商家',
			component: shangjia
		}
		,{
			path: '/cart',
			name: '购物车',
			component: cart
		}
		]
	},
	{
		path: '/shangjiaRegister',
		name: '商家注册',
		component: shangjiaRegister
	},
]

const router = createRouter({
	history: createWebHashHistory(process.env.BASE_URL),
	routes
})

export default router
