<template>
	<div class="app-contain" :style='{"padding":"10px 0","margin":"20px auto 40px","alignItems":"flex-start","borderRadius":"0px","flexWrap":"wrap","background":"#fff","display":"flex","width":"64%","position":"relative","justifyContent":"space-between"}'>
		<div class="bread_view">
			<el-breadcrumb separator="/" class="breadcrumb">
				<el-breadcrumb-item class="first_breadcrumb" :to="{ path: '/' }">首页</el-breadcrumb-item>
				<el-breadcrumb-item class="second_breadcrumb" v-for="(item,index) in breadList" :key="index">{{item.name}}</el-breadcrumb-item>
			</el-breadcrumb>
		</div>
		<div class="back_view">
			<el-button class="back_btn" @click="backClick" type="primary">返回</el-button>
		</div>
		<div class="detail_view">
			<div class="swiper_view">
				<mySwiper :data="bannerList" :type="3"
				:loop="false"
				:navigation="false"
				:pagination="true"
				:paginationType="1"
				:scrollbar="false"
				:slidesPerView="1"
				:spaceBetween="20"
				:autoHeight="false"
				:centeredSlides="false"
				:freeMode="false"
				:effectType="5"
				:direction="horizontal"
				:autoplay="true"
				:slidesPerColumn="1">
				<template #default="scope">
					<img :style='{"border":"1px solid #ddd","width":"100%","objectFit":"contain","height":"480px"}' :src="scope.row?$config.url + scope.row:''">
				</template>
			</mySwiper>
			</div>
			
			<div class="info_view">
				<div class="title_view">
					<div class="detail_title">
						{{detail.shangpinmingcheng}}
					</div>
					<div class="follow" v-if="!collectType" @click="collectClick(1)">
						<i class="iconfont icon-likeline1"></i>
						<span>收藏</span>
					</div>
					<div class="follow" v-if="collectType" @click="collectClick(-1)">
						<i class="iconfont iconfontActive icon-likefill2"></i>
						<span class="textActive">取消收藏</span>
					</div>
				</div>
				<div class="info_item">
					<div class="info_label">价格</div>
					<div class="info_price"><span>￥</span>{{detail.price}}</div>
				</div>
				<div class="info_item" v-if="detail.jf">
					<div class="info_label">积分</div>
					<div class="info_price">{{detail.jf}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">单次购买</div>
					<div class="info_text">{{detail.onelimittimes}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">库存</div>
					<div class="info_text">{{detail.alllimittimes}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">商品分类</div>
					<div  class="info_text" >{{detail.shangpinfenlei}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">产地</div>
					<div  class="info_text" >{{detail.chandi}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">规格</div>
					<div  class="info_text" >{{detail.guige}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">收藏数量</div>
					<div  class="info_text" >{{detail.storeupnum}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">点击次数</div>
					<div  class="info_text" >{{detail.clicknum}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">商家账号</div>
					<div  class="info_text" >{{detail.shangjiazhanghao}}</div>
				</div>
				<div class="info_item">
					<div class="info_label">商家名称</div>
					<div  class="info_text" >{{detail.shangjiamingcheng}}</div>
				</div>
				<div class="btn_view">
					<el-input-number class="inputNumber" v-model="buyNumber" :min="1" v-if="detail.alllimittimes"></el-input-number>
					<el-button class="addCart_btn" v-if="detail.alllimittimes" type="primary" @click="addCart">加入购物车</el-button>
					<el-button class="buyNow_btn" v-if="detail.alllimittimes" type="primary" @click="buyNow">立即购买</el-button>
				</div>
				<div class="btn_view">
					<el-button v-if="centerType&&(detail.ispay=='未支付'||!detail.ispay)&&btnFrontAuth('shangpinxinxi','支付')" class="approval_btn" @click="payClick">支付</el-button>
					<el-button class="edit_btn" v-if="centerType&&btnAuth('shangpinxinxi','修改')" type="primary" @click="editClick">修改</el-button>
					<el-button class="del_btn" v-if="centerType&&btnAuth('shangpinxinxi','删除')" type="danger" @click="delClick">删除</el-button>
				</div>
			</div>
		</div>
		<el-tabs type="border-card" v-model="activeName" class="tabs_view">
			<el-tab-pane label="商品详情" name="first">
				<div v-html="detail.shangpinxiangqing"></div>
			</el-tab-pane>
			<el-tab-pane label="评论" name="commentActive">
				<div class="my_comment_view">
					<el-form ref="commentFormRef" :model="commentForm" class="my_comment_form"
						:rules="commentRules">
						<el-form-item prop="content">
							<el-input class="comment_inp" v-model="commentForm.content" type="textarea"
								placeholder="请输入评论内容"></el-input>
						</el-form-item>
					</el-form>
					<div class="comment_btn">
						<el-button class="add_btn" type="primary" @click="commentSave">立即评论</el-button>
						<el-button class="reset_btn" @click="resetForm">重置</el-button>
					</div>
				</div>
				<div class="comment_list">
					<div class="comment" v-for="(item,index) in commentList" :key="index">
						<div class="comment_top">
							<div class="comment_user">
								<div class="comment_user_img">
									<img :src="item.avatarurl?$config.url + item.avatarurl:'../../../assets/avatar.png'" alt="">
								</div>
								<div class="comment_user_info">
									{{item.nickname}}
								</div>
							</div>
							<div class="comment_time">{{item.addtime}}</div>
						</div>
						<div class="comment_bottom">
							<div class="comment_content">{{item.content}}</div>
							<div class="comment_reply" v-if="item.reply">
								回复：{{item.reply}}
							</div>
						</div>
					</div>
				</div>
				<el-pagination
					background 
					:layout="layouts.join(',')"
					:total="commentTotal" 
					:page-size="commentQuery.limit"
					prev-text="上一页"
					next-text="下一页"
					:hide-on-single-page="false"
					:style='{"border":"0px solid #eee","padding":"4px 0","margin":"10px 0 20px","whiteSpace":"nowrap","color":"#333","textAlign":"center","flexWrap":"wrap","background":"none","display":"flex","width":"100%","fontWeight":"500","justifyContent":"center"}'
					@size-change="commentSizeChange"
					@current-change="commentCurrentChange" 
					@prev-click="commentPrevClick"
					@next-click="commentNextClick"  />
			</el-tab-pane>
		</el-tabs>
	</div>
</template>
<script setup>
	import axios from 'axios'
	import {
		ref,
		getCurrentInstance,
		watch,
		onUnmounted,
		onMounted,
		nextTick,
		computed
	} from 'vue';
	import {
		ElMessageBox
	} from 'element-plus'
	import {
		useRoute,
		useRouter
	} from 'vue-router';
	const context = getCurrentInstance()?.appContext.config.globalProperties;
	const route = useRoute()
	const router = useRouter()
	//基础信息
	const tableName = 'shangpinxinxi'
	const formName = '商品信息'
	//基础信息
	const breadList = ref([{
		name: formName
	}])
	//权限验证
	const btnAuth = (e,a)=>{
		if(centerType.value){
			return context?.$toolUtil.isBackAuth(e,a)
		}else{
			return context?.$toolUtil.isAuth(e,a)
		}
	}
	//查看权限验证
	const btnFrontAuth = (e,a)=>{
		if(centerType.value){
			return context?.$toolUtil.isBackAuth(e,a)
		}else{
			return context?.$toolUtil.isFrontAuth(e,a)
		}
	}
	// 返回
	const backClick = () =>{
		history.back()
	}
	// 轮播图
	const bannerList = ref([])
	// 详情
	const title = ref('')
	const detail = ref({})
    const activeName = ref('first')
	const getDetail = () => {
		context?.$http({
			url: `${tableName}/detail/${route.query.id}`,
			method: 'get'
		}).then(res => {
			title.value = res.data.data.shangpinmingcheng
			bannerList.value = res.data.data.shangpintupian?res.data.data.shangpintupian.split(','):[]
			detail.value = res.data.data
			getInCartList();
		})
	}
	// 下载文件
	const downClick = (file) => {
		if(!file){
			context?.$toolUtil.message('文件不存在','error')
		}
		let arr = file.replace(new RegExp('file/', "g"), "")
		axios.get((location.href.split(context?.$config.name).length>1 ? location.href.split(context?.$config.name)[0] :'') + context?.$config.name + '/file/download?fileName=' + arr, {
			headers: {
				token: context?.$toolUtil.storageGet('frontToken')
			},
			responseType: "blob"
		}).then(({
			data
		}) => {
			const binaryData = [];
			binaryData.push(data);
			const objectUrl = window.URL.createObjectURL(new Blob(binaryData, {
				type: 'application/pdf;chartset=UTF-8'
			}))
			const a = document.createElement('a')
			a.href = objectUrl
			a.download = arr
			// a.click()
			// 下面这个写法兼容火狐
			a.dispatchEvent(new MouseEvent('click', {
				bubbles: true,
				cancelable: true,
				view: window
			}))
			window.URL.revokeObjectURL(data)
		})
	}
	// 判断是否从个人中心跳转
	const centerType = ref(false)
	const init = () => {
		if(route.query.centerType){
			centerType.value = true
		}
		getDetail()
		// 收藏
		getCollect()
		// 评论
		getCommentList()
	}
	// 收藏
	const collectType = ref(false)
	const collectInfo = ref({})
	const getCollect = () => {
		if (context?.$toolUtil.storageGet('frontToken')) {
			context?.$http({
				url: 'storeup/list',
				method: 'get',
				params: {
					page: 1,
					limit: 1,
					type: 1,
					refid: route.query.id,
					tablename: tableName,
					userid: context?.$toolUtil.storageGet('userid')
				}
			}).then(res => {
				if (res.data.data.list.length) {
					collectType.value = true
					collectInfo.value = res.data.data.list[0]
				}else{
					collectType.value = false
					collectInfo.value = {}
				}
			})
		}
	}
	// 收藏按钮
	const collectClick = (type) => {
		if (type == 1 && !collectType.value) {
			let params = {
				name: title.value,
				picture: bannerList.value[0],
				refid: detail.value.id,
				type: type,
				tablename: tableName,
				userid: context?.$toolUtil.storageGet('userid')
			}
			context?.$http({
				url: 'storeup/add',
				method: 'post',
				data: params
			}).then(res => {
				detail.value.storeupnum += 1
				context?.$http({
					url: `${tableName}/update`,
					method: 'post',
					data: detail.value
				})
				collectType.value = true
				getCollect()
				context?.$toolUtil.message('收藏成功', 'success')
			})
		}
		else if (type == -1 && collectType.value) {
			let ids = []
			ids.push(collectInfo.value.id)
			context?.$http({
				url: 'storeup/delete',
				method: 'post',
				data: ids
			}).then(res => {
				detail.value.storeupnum -= 1
				context?.$http({
					url: `${tableName}/update`,
					method: 'post',
					data: detail.value
				})
				collectInfo.value = {}
				collectType.value = false
				context?.$toolUtil.message('取消成功', 'success')
			})
		}
	}
	//评论
	const commentForm = ref({
		content: '',
		refid: route.query.id,
		userid: context?.$toolUtil.storageGet('userid'),
		nickname: context?.$toolUtil.storageGet('frontName'),
		avatarurl: context?.$toolUtil.storageGet('headportrait') ? context?.$toolUtil.storageGet('headportrait') : ''
	})
	const commentRules = ref({
		content: [{
			required: true,
			message: '请输入',
			trigger: 'blur'
		}, ]
	})
	const commentQuery = ref({
		page: 1,
		limit: 10,
		refid: route.query.id
	})
	const layouts = ref(["prev","pager","next","total"])
	const commentList = ref([])
	const commentTotal = ref(0)
	const commentFormRef = ref(null)
	const commentSizeChange = (size) =>{
		commentQuery.value.limit = size
		getCommentList()
	}
	const commentCurrentChange = (page) =>{
		commentQuery.value.page = page
		getCommentList()
	}
	const commentPrevClick = () =>{
		commentQuery.value.page = commentQuery.value.page - 1
		getCommentList()
	}
	const commentNextClick = () =>{
		commentQuery.value.page = commentQuery.value.page + 1
		getCommentList()
	}
	const getCommentList = () => {
		context?.$http({
			url: `discuss${tableName}/list`,
			params: commentQuery.value,
			method: 'get'
		}).then(res => {
			commentList.value = res.data.data.list
			commentTotal.value = res.data.data.total
	
		})
	}
	//提交评论
	const commentSave = () => {
		let sensitiveWords = "";
		let sensitiveWordsArr = [];
		if(sensitiveWords) {
		    sensitiveWordsArr = sensitiveWords.split(",");
		}
		for(var i=0; i<sensitiveWordsArr.length; i++){
		    //全局替换
		    var reg = new RegExp(sensitiveWordsArr[i],"g");
		    //判断内容中是否包括敏感词
		    if (commentForm.value.content.indexOf(sensitiveWordsArr[i]) > -1) {
		        // 将敏感词替换为 **
		        commentForm.value.content = commentForm.value.content.replace(reg,"**");
		    }
		}
		commentFormRef.value.validate((valid) => {
			if (valid) {
				context?.$http({url:'orders/list',method:'get',params:{page:1,limit:1,status:'已完成',goodid:detail.value.id,userid:context?.$toolUtil.storageGet('userid')}}).then(res=>{
					if(res.data.data.list.length==0){
						context?.$toolUtil.message('请完成订单后再评论！','error')
						return false
					}
					context?.$http({
						url: `discuss${tableName}/add`,
						method: 'post',
						data: commentForm.value
					}).then(res => {
						context?.$toolUtil.message('评论成功', 'success', () => {
							resetForm()
							getCommentList()
						})
					})
				})
			}
		})
	}
	const resetForm = () => {
		commentFormRef.value.resetFields()
	}
	//查找是否已添加购物车
	const isInCart = ref(false)
	const getInCartList = () => {
		context?.$http({url:'cart/list',method:'get',params:{userid:context?.$toolUtil.storageGet('userid'),tablename:tableName,goodid: detail.value.id}}).then(res=>{
			if(res.data.data.list.length){
				isInCart.value = true
			}else{
				isInCart.value = false
			}
		})
	}
	const cartForm = ref({
		userid:context?.$toolUtil.storageGet('userid')
	})
	const addCart = () => {
		//单次购买限制
		if(detail.value.onelimittimes > 0 && detail.value.onelimittimes < buyNumber.value){
			context?.$toolUtil.message(`每人只能购买${detail.value.onelimittimes}件商品`,'error')
			return false
		}
		//库存不足
		if(detail.value.alllimittimes <= 0){
			context?.$toolUtil.message('商品售罄','error')
			return false
		}
		//库存限制
		if(detail.value.alllimittimes > 0 && detail.value.alllimittimes < buyNumber.value){
			context?.$toolUtil.message('库存不足','error')
			return false
		}
		if(isInCart.value){
			context?.$toolUtil.message('该商品已在购物车，请前往购买','error')
			return false
		}
		cartForm.value.buynumber = buyNumber.value
		cartForm.value.goodid = detail.value.id
		cartForm.value.goodname = title.value
		cartForm.value.tablename = tableName
		cartForm.value.picture = bannerList.value[0]
		cartForm.value.shangjiazhanghao = detail.value.shangjiazhanghao
        cartForm.value.price = detail.value.price;
		context?.$http({
			url: 'cart/save',
			method: 'post',
			data:cartForm.value
		}).then(res=>{
			context?.$toolUtil.message('添加成功','success')
			getInCartList()
		})
	}
	const buyNumber = ref(1)
	//立即购买
	const buyNow = () => {
		//单次购买限制
		if(detail.value.onelimittimes > 0 && detail.value.onelimittimes < buyNumber.value){
			context?.$toolUtil.message(`每人只能购买${detail.value.onelimittimes}件商品`,'error')
			return false
		}
		//库存不足
		if(detail.value.alllimittimes <= 0){
			context?.$toolUtil.message('商品售罄','error')
			return false
		}
		//库存限制
		if(detail.value.alllimittimes > 0 && detail.value.alllimittimes < buyNumber.value){
			context?.$toolUtil.message('库存不足','error')
			return false
		}
		let data = {
			tablename: tableName,
			goodid: detail.value.id,
			goodname: title.value,
			buynumber: buyNumber.value,
			userid: context?.$toolUtil.storageGet('userid'),
			discountprice: detail.value.vipprice ? detail.value.vipprice : 0,
			shangjiazhanghao: detail.value.shangjiazhanghao,
			picture: bannerList.value[0],
			price: detail.value.price
		}
		context?.$toolUtil.storageSet('orders_good',JSON.stringify([data]))
		let query = {
			type: 1
		}
		router.push({path: '/index/order_confirm', query: query})
	}
	//修改
	const editClick = () => {
		router.push(`/index/${tableName}Add?id=${detail.value.id}&&type=edit`)
	}
	//删除
	const delClick = () => {
		ElMessageBox.confirm(`是否删除此${formName}？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(()=>{
			context?.$http({
				url: `${tableName}/delete`,
				method: 'post',
				data: [detail.value.id]
			}).then(res=>{
				context?.$toolUtil.message('删除成功','success',()=>{
					history.back()
				})
			})
			
		})
	}
	onMounted(()=>{
		init()
	})
</script>
<style lang="scss" scoped>
	// 返回盒子
	.back_view {
		border-radius: 0px;
		padding: 0 20px;
		margin: 0 auto 20px;
		background: none;
		display: block;
		width: 100%;
		text-align: right;
		// 返回按钮
		.back_btn {
			border: 1px solid #eee;
			cursor: pointer;
			border-radius: 4px;
			padding: 0 30px;
			outline: none;
			color: #666;
			background: #f9f9f9;
			width: auto;
			font-size: 14px;
			height: 32px;
		}
		// 返回按钮-悬浮
		.back_btn:hover {
		}
	}
	// 面包屑盒子
	.bread_view {
		border-radius: 0px;
		padding: 12px 20px;
		margin: 0px auto;
		background: none;
		width: 100%;
		border-color: #eee;
		border-width: 0 0 0px;
		position: relative;
		border-style: solid;
		:deep(.breadcrumb) {
			font-size: 14px;
			line-height: 1;
			.el-breadcrumb__separator {
				margin: 0 9px;
				color: #999;
				font-weight: 500;
			}
			.first_breadcrumb {
				.el-breadcrumb__inner {
					color: #333;
					display: inline-block;
				}
			}
			.second_breadcrumb {
				.el-breadcrumb__inner {
					color: #999;
					display: inline-block;
				}
			}
		}
	}
	
	.detail_view{
		border-radius: 0;
		padding: 20px 0;
		background: none;
		display: flex;
		width: 100%;
		border-color: #ddd;
		border-width: 0px;
		justify-content: space-between;
		position: relative;
		border-style: solid;
		flex-wrap: wrap;
		// 轮播图
		.swiper_view {
			padding: 0;
			margin: 0 auto 20px;
			background: none;
			width: 480px;
			float: right;
			height: 480px;
			order: 0;
		}
		
		// 文字区
		.info_view {
			border: 0px solid #eee;
			padding: 0px 4%;
			margin: 0 0 20px;
			background: none;
			width: calc(100% - 520px);
			min-height: 550px;
			box-sizing: border-box;
			float: left;
		
			.title_view {
				border: 1px solid #b5b5b5;
				padding: 0 20px;
				margin: 0 0 12px;
				background: #eee;
				display: flex;
				width: 100%;
				line-height: 40px;
				justify-content: space-between;
				align-items: center;
		
				.detail_title {
					color: #333;
					font-weight: 600;
					font-size: 16px;
				}
				// 收藏盒子
				.follow {
					border: 0px solid #ffffff50;
					cursor: pointer;
					border-radius: 4px;
					padding: 4px 10px;
					color: #fff;
					background: none;
					display: flex;
					width: auto;
					line-height: 1;
					justify-content: center;
					align-items: center;
					.iconfont {
						margin: 0 4px 0 0;
						color: #f60;
						font-size: 18px;
					}
					.iconfontActive {
						margin: 0 4px 0 0;
						color: #ff0;
						font-size: 18px;
					}
					span {
						color: #f60;
						font-size: 14px;
					}
					.textActive {
						color: #ff0;
						font-size: 14px;
					}
				}
				.follow:hover {
				}
				.follow:active {
					transform: scale(0.9);
				}
			}
		
			.info_item {
					border-radius: 0px;
					padding: 0 0 10px;
					margin: 0 0 10px;
					background: none;
					display: flex;
					border-color: #eee;
					border-width: 0 0 1px;
					align-items: center;
					border-style: dashed;
		
				.info_label {
					margin: 0 12px 0 0;
					color: #666;
					font-weight: 500;
					width: auto;
				}
				.info_text {
				}
			}
			.btn_view {
				padding: 0;
				margin: 20px 0 20px;
				display: block;
				width: 100%;
				flex-wrap: wrap;
				// 数量输入框
				.inputNumber {
					margin: 0 5px 10px 0;
					display: block;
					width: 150px;
					line-height: 32px;
					position: relative;
					:deep(.el-input-number__decrease) {
						cursor: pointer;
						z-index: 1;
						display: flex;
						border-color: #DCDFE6;
						border-radius: 4px 0 0 4px;
						top: 1px;
						left: 1px;
						background: #f5f5f5;
						width: 40px;
						justify-content: center;
						border-width: 0 1px 0 0;
						align-items: center;
						position: absolute;
						border-style: solid;
						text-align: center;
						height: 30px;
						i {
							color: #666;
							font-size: 14px;
						}
					}
					:deep(.el-input-number__increase) {
						cursor: pointer;
						z-index: 1;
						display: flex;
						border-color: #DCDFE6;
						right: 1px;
						border-radius: 0 4px 4px 0;
						top: 1px;
						background: #f5f5f5;
						width: 40px;
						justify-content: center;
						border-width: 0 0 0 1px;
						align-items: center;
						position: absolute;
						border-style: solid;
						text-align: center;
						height: 30px;
						i {
							color: #666;
							font-size: 14px;
						}
					}
					:deep(.el-input) {
						.el-input__wrapper {
							border: 1px solid #DCDFE6;
							border-radius: 4px;
							padding: 0 20px;
							outline: none;
							color: #666;
							background: #FFF;
							display: inline-block;
							width: 100%;
							font-size: 14px;
							line-height: 32px;
							text-align: center;
							height: 32px;
						}
					}
				}
				// 加入购物车-按钮
				.addCart_btn {
					border: 0;
					padding: 0 0px;
					color: #113961;
					background: none;
					display: inline-block;
					line-height: 32px;
					transition: all 0.3s;
					height: 32px;
				}
				// 悬浮
				.addCart_btn:hover {
					transform: rotate(360deg);
				}
				// 立即购买-按钮
				.buyNow_btn {
					border: 0px solid rgba(135, 142, 249, 1);
					padding: 0 0px;
					color: #007fc6;
					background: none;
					line-height: 32px;
					transition: all 0.3s;
					height: 32px;
				}
				// 悬浮
				.buyNow_btn:hover {
					transform: rotate(360deg);
				}
				// 修改-按钮
				.edit_btn {
					border: none;
					padding: 0 0px;
					color: #2da065;
					background: none;
					line-height: 32px;
					transition: all 0.3s;
					height: 32px;
				}
				// 悬浮
				.edit_btn:hover {
					transform: rotate(360deg);
				}
				// 删除-按钮
				.del_btn {
					border: none;
					padding: 0 0px;
					color: #f68b8b;
					background: none;
					line-height: 32px;
					transition: all 0.3s;
					height: 32px;
				}
				// 悬浮
				.del_btn:hover {
					transform: rotate(360deg);
				}
			}
		}
	}
	

	//底部盒子
	.tabs_view {
		border: 0px solid #ddd;
		border-radius: 0px;
		padding: 0;
		box-shadow: none;
		margin: 30px auto;
		background: none;
		width: 100%;
		:deep(.el-tabs__header) {
			background: transparent;
			border: none;
		}
		// 头部
		:deep(.el-tabs__nav-scroll) {
			border-radius: 0;
			padding: 2px 20px 0;
			margin: 0;
			background: none;
			display: flex;
			border-color: #ddd;
			border-width: 0px 0px 0px;
			border-style: solid;
			flex-wrap: wrap;
			height: 50px;
			.el-tabs__nav {
				.el-tabs__item {
					border: 1px solid #b5b5b5;
					padding: 0 20px;
					margin: 0 10px 0 0;
					color: #333;
					font-weight: 500;
					display: inline-block;
					font-size: 14px;
					line-height: 48px;
					transition: all 0.3s;
					border-radius: 5px;
					background: #eee;
					position: relative;
					list-style: none;
					text-align: center;
					min-width: 100px;
					height: 48px;
				}
				.el-tabs__item:hover {
					border: 1px solid #113961;
					color: #fff;
					background: #113961;
				}
				.is-active {
					border: 1px solid #113961;
					border-radius: 5px;
					padding: 0 20px;
					margin: 0 10px 0 0;
					color: #fff;
					background: #113961;
					line-height: 48px;
					text-align: center;
					min-width: 100px;
					height: 48px;
				}
			}
		}
		// 内容区
		:deep(.el-tabs__content) {
			border-radius: 4px;
			padding: 20px;
			margin: 20px;
			color: #666;
			background: none;
			font-size: 14px;
			border-color: #eee;
			border-width: 0px;
			border-style: solid;
		}
		//评论
		//我的评论
		.my_comment_view {
			border: 0px solid #eee;
			border-radius: 4px;
			box-shadow: none;
			padding: 0px;
			margin: 0px;
			background: none;
			width: 100%;
		
			.my_comment_form {
				border: 0px solid #eee;
				border-radius: 0px;
				padding: 0px;
				box-shadow: none;
				background: #fff;
				width: 100%;
				box-sizing: border-box;
				// 输入框
				:deep(.el-textarea__inner) {
					border: 1px solid #eee;
					border-radius: 0;
					padding: 12px;
					box-shadow: none;
					color: #333;
					width: 100%;
					font-size: 14px;
					min-height: 120px;
				}
			}
			// 按钮盒子
			.comment_btn {
				margin: 20px 0 0;
				display: flex;
				width: 100%;
				justify-content: center;
				align-items: center;
				// 评论按钮
				.add_btn {
					border: none;
					border-radius: 4px;
					padding: 0 20px;
					margin: 0 20px 0 0;
					color: #fff;
					background: linear-gradient(180deg, rgba(108,153,198,1) 0%, rgba(17,57,97,1) 100%),#113961;
					line-height: 36px;
					transition: all 0.3s;
					height: 36px;
				}
				// 悬浮
				.add_btn:hover {
					transform: rotate(360deg);
				}
				// 重置按钮
				.reset_btn {
					border: 0px solid #666;
					border-radius: 4px;
					padding: 0 20px;
					color: #fff;
					background: #999;
					line-height: 36px;
					transition: all 0.3s;
					min-width: 100px;
					height: 36px;
				}
				// 悬浮
				.reset_btn:hover {
					transform: rotate(360deg);
				}
			}
		}
		
		//评论列表
		.comment_list {
			border: 0px solid #eee;
			border-radius: 0px;
			padding: 0px;
			margin: 30px auto;
			background: none;
			display: flex;
			width: 100%;
			justify-content: space-between;
			flex-wrap: wrap;
		
			.comment {
				border: 0px solid #2da06530;
				border-radius: 4px;
				padding: 0 0 10px;
				margin: 0 0 20px;
				background: none;
				width: 32%;
				box-sizing: border-box;
				.comment_top {
					border-radius: 0px;
					padding: 0 0 0px;
					background: #eee;
					display: flex;
					width: 100%;
					font-size: 14px;
					border-color: #b5b5b5;
					border-width: 1px;
					justify-content: space-between;
					align-items: center;
					border-style: solid;
		
					.comment_user {
						border-radius: 4px;
						padding: 4px 30px 4px 10px;
						display: block;
						align-items: center;
						.comment_user_img {
							margin: 0 10px 0 0;
							font-size: 0;
							float: left;
		
							img {
								border-radius: 50%;
								width: 40px;
								height: 40px;
							}
						}
						.comment_user_info {
							color: #333;
							font-weight: 600;
							font-size: 14px;
							line-height: 40px;
							float: left;
						}
					}
					.comment_time{
						border: 0px solid #eee;
						border-radius: 4px;
						padding: 0px 20px;
						color: #999;
						font-size: 14px;
						float: right;
					}
				}
		
				.comment_bottom {
					border: 0px solid #eee;
					border-radius: 4px;
					padding: 0px 10px;
					margin: 10px 0 0;
					flex-direction: column;
					display: flex;
					width: 100%;
					align-items: flex-start;
					.comment_content {
						color: #666;
						font-weight: 600;
						font-size: 14px;
					}
					.comment_reply {
						padding: 10px 0 0;
						flex-direction: column;
						color: #999;
						text-indent: 2rem;
						display: flex;
						width: 100%;
						font-size: 14px;
						align-items: flex-start;
					}
				}
			}
		}
		// 分页器
		.el-pagination {
			// 总页码
			:deep(.el-pagination__total) {
				margin: 0 10px 0;
				color: #666;
				font-weight: 400;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 24px;
				height: 24px;
			}
			// 上一页
			:deep(.btn-prev) {
				border: 1px solid #ddd;
				border-radius: 2px;
				padding: 0 4px;
				margin: 0 2px;
				color: #666;
				background: none;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 24px;
				min-width: 24px;
				height: 24px;
			}
			// 下一页
			:deep(.btn-next) {
				border: 1px solid #ddd;
				border-radius: 2px;
				padding: 0 4px;
				margin: 0 2px;
				color: #666;
				background: none;
				display: inline-block;
				vertical-align: top;
				font-size: 14px;
				line-height: 24px;
				min-width: 24px;
				height: 24px;
			}
			// 上一页禁用
			:deep(.btn-prev:disabled) {
				border: 1px solid #ddd;
				cursor: not-allowed;
				padding: 0 4px;
				margin: 0 2px;
				color: #C0C4CC;
				display: inline-block;
				vertical-align: top;
				font-size: 14px;
				line-height: 24px;
				border-radius: 2px;
				background: none;
				min-width: 24px;
				height: 24px;
			}
			// 下一页禁用
			:deep(.btn-next:disabled) {
				border: 1px solid #ddd;
				cursor: not-allowed;
				padding: 0 4px;
				margin: 0 2px;
				color: #C0C4CC;
				display: inline-block;
				vertical-align: top;
				font-size: 14px;
				line-height: 24px;
				border-radius: 2px;
				background: none;
				min-width: 24px;
				height: 24px;
			}
			// 页码
			:deep(.el-pager) {
				padding: 0;
				margin: 0;
				display: inline-block;
				vertical-align: top;
				// 数字
				.number {
					cursor: pointer;
					padding: 0 4px;
					margin: 0 5px;
					color: #666;
					display: inline-block;
					vertical-align: top;
					font-size: 13px;
					line-height: 24px;
					border-radius: 2px;
					background: #f4f4f5;
					text-align: center;
					min-width: 24px;
					height: 24px;
				}
				// 数字悬浮
				.number:hover {
					cursor: pointer;
					padding: 0 4px;
					margin: 0 5px;
					color: #fff;
					display: inline-block;
					vertical-align: top;
					font-size: 13px;
					line-height: 24px;
					border-radius: 2px;
					background: #113961;
					text-align: center;
					min-width: 24px;
					height: 24px;
				}
				// 选中
				.number.is-active {
					cursor: default;
					padding: 0 4px;
					margin: 0 5px;
					color: #fff;
					display: inline-block;
					vertical-align: top;
					font-size: 13px;
					line-height: 24px;
					border-radius: 2px;
					background: #113961;
					text-align: center;
					min-width: 24px;
					height: 24px;
				}
			}
			// sizes
			:deep(.el-pagination__sizes) {
				box-shadow: none;
				margin: 0 0 0 5px;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 24px;
				height: 24px;
				.el-select {
					border: 0px solid #DCDFE6;
					cursor: pointer;
					padding: 0;
					color: #606266;
					display: inline-block;
					font-size: 13px;
					line-height: 24px;
					border-radius: 2px;
					outline: 0;
					background: #f4f4f5;
					width: 100%;
					text-align: center;
					height: 24px;
				//去掉默认样式
				.select-trigger{
					height: 100%;
					.el-input{
						height: 100%;
						.el-input__wrapper{
							border: none;
							box-shadow: none;
							background: none;
							border-radius: 0;
							height: 100%;
							padding: 0;
						}
						.is-focus {
							box-shadow: none !important;
						}
					}
				}
				}
			}
			// 跳页
			:deep(.el-pagination__jump) {
				margin: 0 0 0 24px;
				color: #606266;
				display: inline-block;
				vertical-align: top;
				font-size: 13px;
				line-height: 28px;
				height: 28px;
				// 输入框
				.el-input {
					border: 1px solid #DCDFE6;
					cursor: pointer;
					padding: 0 3px;
					margin: 0 6px;
					color: #606266;
					display: inline-block;
					font-size: 14px;
					line-height: 24px;
					border-radius: 3px;
					outline: 0;
					background: #FFF;
					width: 38px;
					text-align: center;
					height: 24px;
					//去掉默认样式
					.el-input__wrapper{
						border: none;
						box-shadow: none;
						background: none;
						border-radius: 0;
						height: 100%;
						padding: 0;
					}
					.is-focus {
						box-shadow: none !important;
					}
				}
			}
		}
	}
	


</style>