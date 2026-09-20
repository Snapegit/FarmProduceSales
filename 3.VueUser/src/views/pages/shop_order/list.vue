<template>
	<div class="app-contain" :style='{"width":"64%","padding":"0px 20px 10px","margin":"30px auto 40px","position":"relative","borderRadius":"0px","background":"#fff"}'>
		<div class="section_title">
			{{formName}}
		</div>
		<el-tabs v-model="orderStatus" type="card" class="demo-tabs" @tab-change="statusChange">
			<el-tab-pane label="全部" :name="''"></el-tab-pane>
			<el-tab-pane label="未支付" name="未支付"></el-tab-pane>
			<el-tab-pane label="已支付" name="已支付"></el-tab-pane>
			<el-tab-pane label="已发货" name="已发货"></el-tab-pane>
			<el-tab-pane label="已完成" name="已完成"></el-tab-pane>
			<el-tab-pane label="已退款" name="已退款"></el-tab-pane>
			<el-tab-pane label="已取消" name="已取消"></el-tab-pane>
		</el-tabs>
		<el-table v-loading="listLoading" border :stripe='false' @selection-change="handleSelectionChange" ref="table"
			:data="list" @row-click="listChange">
			<el-table-column type="selection" width="55" :resizable='true' align="left" header-align="left" />
			<el-table-column label="序号" width="120" :resizable='true' align="left" header-align="left">
				<template #default="scope">{{ scope.$index + 1}}</template>
			</el-table-column>
			<el-table-column label="订单编号" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.orderid}}
				</template>
			</el-table-column>
			<el-table-column label="商品" width="200px" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					<div style="display: flex;align-items: center;">
						<div v-if="scope.row.picture">
							<el-image v-if="scope.row.picture.substring(0,4)=='http'" preview-teleported
								:preview-src-list="[scope.row.picture.split(',')[0]]"
								:src="scope.row.picture.split(',')[0]" style="width:100px;height:100px"></el-image>
							<el-image v-else preview-teleported
								:preview-src-list="[$config.url+scope.row.picture.split(',')[0]]"
								:src="$config.url+scope.row.picture.split(',')[0]" style="width:100px;height:100px">
							</el-image>
						</div>
						<div v-else>无图片</div>
						<span style="margin-left: 10px;">{{scope.row.goodname}}</span>
					</div>
				</template>
			</el-table-column>
			<el-table-column label="购买数量" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.buynumber}}
				</template>
			</el-table-column>
			<el-table-column label="价格" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					<span v-if="scope.row.type!=2" style="font-size: 12px;">￥</span>{{scope.row.price}} <span
						v-if="scope.row.type==2">积分</span>
				</template>
			</el-table-column>
			<el-table-column label="总价" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					<span v-if="scope.row.type!=2" style="font-size: 12px;">￥</span>{{scope.row.total}} <span
						v-if="scope.row.type==2">积分</span>
				</template>
			</el-table-column>
			<el-table-column label="地址" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.address}}
				</template>
			</el-table-column>
			<el-table-column label="电话" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.tel}}
				</template>
			</el-table-column>
			<el-table-column label="收货人" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.consignee}}
				</template>
			</el-table-column>
			<el-table-column label="备注" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.remark}}
				</template>
			</el-table-column>

			<el-table-column label="下单时间" :resizable='true' align="left" header-align="left">
				<template #default="scope">
					{{scope.row.addtime}}
				</template>
			</el-table-column>
			<el-table-column label="操作" :resizable='true' align="left" header-align="left" width="200px">
				<template #default="scope">
					<el-button class="pay_btn" v-if="scope.row.status=='未支付'" type="primary" @click="payClick(scope.row)">
						余额支付
					</el-button>
					<el-button class="cancel_btn" v-if="scope.row.status=='未支付'" type="danger" @click="cancelClick(scope.row)">
						取消
					</el-button>
					<el-button class="refundPrice_btn" v-if="scope.row.status=='已支付'" type="danger" @click="refundPriceClick(scope.row)">
						退款
					</el-button>
					<el-button class="refundGood_btn" v-if="scope.row.status=='已完成'" type="danger" @click="refundGoodClick(scope.row)">
						退货
					</el-button>
					<el-button class="logistics_btn" v-if="scope.row.logistics" type="primary" @click="logisticsClick(scope.row)">
						物流
					</el-button>
					<el-button class="confirm_btn" v-if="scope.row.status=='已发货'" type="success" @click="confirmGoodClick(scope.row)">
						确认收货
					</el-button>
					<el-button class="toDetail_btn" v-if="scope.row.status=='已完成'" type="warning" @click="toDetailClick(scope.row)">
						评论
					</el-button>
				</template>
			</el-table-column>
		</el-table>
		<el-pagination
			background 
			:layout="layouts.join(',')"
			:total="total" 
			:page-size="listQuery.limit"
			prev-text="上一页"
			next-text="下一页"
			:hide-on-single-page="false"
			:style='{"border":"0px solid #eee","padding":"4px 0","margin":"10px 0 20px","whiteSpace":"nowrap","color":"#333","textAlign":"center","flexWrap":"wrap","background":"none","display":"flex","width":"100%","fontWeight":"500","justifyContent":"center"}'
			@size-change="sizeChange"
			@current-change="currentChange" 
			@prev-click="prevClick"
			@next-click="nextClick"  />
		<el-dialog v-model="logisticsVisible" title="物流信息" width="70%">
			<div v-html="logisticsText"></div>
		</el-dialog>
	</div>
</template>

<script setup>
	import {
		ref,
		getCurrentInstance,
		nextTick,
	} from 'vue';
	import {
		ElMessageBox
	} from 'element-plus'
	import {
		useRoute,
		useRouter
	} from 'vue-router';
	const context = getCurrentInstance()?.appContext.config.globalProperties;
	const router = useRouter()
	const route = useRoute()
	//基础信息
	const tableName = 'orders'
	const formName = '商品订单'
	const table = ref(null)
	const selRows = ref([])
	const list = ref([])
	const listLoading = ref(false)
	const listQuery = ref({
		page: 1,
		limit: 20,
		userid: context?.$toolUtil.storageGet('userid')
	})

	//多选
	const handleSelectionChange = (e) => {
		selRows.value = e
	}
	//列表数据
	const getList = () => {
		listLoading.value = true
		let params = JSON.parse(JSON.stringify(listQuery.value))
		if (orderStatus.value) {
			params['status'] = orderStatus.value
		}
		context?.$http({
			url: 'orders/list',
			method: 'get',
			params: params
		}).then(res => {
			listLoading.value = false
			list.value = res.data.data.list
			total.value = Number(res.data.data.total)
		})
	}
	//分页
	const layouts = ref(["prev","pager","next","total"])
	const total = ref(0)
	const sizeChange = (size) => {
		listQuery.value.limit = size
		getList()
	}
	const currentChange = (page) => {
		listQuery.value.page = page
		getList()
	}
	const prevClick = () => {
		listQuery.value.page = listQuery.value.page - 1
		getList()
	}
	const nextClick = () => {
		listQuery.value.page = listQuery.value.page + 1
		getList()
	}
	//分页
	//基础信息
	const orderStatus = ref('')
	//tab切换
	const statusChange = () => {
		listQuery.value.page = 1
		getList()
	}
	//余额支付
	const payClick = (row) => {
		ElMessageBox.confirm(`是否确定支付该订单？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			context?.$http({
				url: `${row.tablename}/info/${row.goodid}`,
				method: 'get'
			}).then(res => {
				let data = res.data.data
				if (userinfo.value.money < row.total) {
					context?.$toolUtil.message('余额不足', 'error')
					return
				}
				//如果商品存在积分，则累加用户积分
				if (data.jf) {
					userinfo.value.jf = parseInt(userinfo.value.jf) + parseInt(row.total)
				}
				//减去用户余额
				userinfo.value.money = (parseFloat(userinfo.value.money) - parseFloat(row.total)).toFixed(2)
				//如果商品存在库存，则减去商品库存
				if (data.alllimittimes) {
					data.alllimittimes = parseInt(data.alllimittimes) - parseInt(row.buynumber)
					//更新商品库存
					context?.$http({
						url: `${row.tablename}/update`,
						method: 'post',
						data: data
					}).then(obj1 => {})
				}
				//更新用户信息
				context?.$http({
					url: `${context?.$toolUtil.storageGet('frontSessionTable')}/update`,
					method: 'post',
					data: userinfo.value
				}).then(obj => {
					row.status = '已支付'
					//修改订单状态
					context?.$http({
						url: 'orders/update',
						method: 'post',
						data: row
					}).then(res1 => {
						context?.$toolUtil.message('支付成功', 'success', () => {
							getSession()
							statusChange()
						})
					})
				})
			})
		})
	}
	//取消订单
	const cancelClick = (row) => {
		ElMessageBox.confirm(`是否取消该订单？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			//未完成支付，未减去用户余额，未减去库存，未累加积分，则不需要用户操作跟商品库存操作
			row.status = '已取消'
			//修改订单状态
			context?.$http({
				url: 'orders/update',
				method: 'post',
				data: row
			}).then(res1 => {
				context?.$toolUtil.message('取消成功', 'success', () => {
					getSession()
					statusChange()
				})
			})
		})
	}
	// 退款
	const refundPriceClick = (row) => {
		ElMessageBox.confirm(`是否对该订单进行退款操作？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			context?.$http({
				url: `${row.tablename}/info/${row.goodid}`,
				method: 'get'
			}).then(res => {
				let data = res.data.data
				// 如果商品存在库存，则加回去
				if (data.alllimittimes) {
					data.alllimittimes = parseInt(data.alllimittimes) + parseInt(row.buynumber)
					// 更新商品库存
					context?.$http({
						url: `${row.tablename}/update`,
						method: 'post',
						data: data
					}).then(obj1 => {})
				}
				if (row.type == 2) {
					// 如果是积分兑换，则把减去的积分加回去
					userinfo.value.jf = parseInt(userinfo.value.jf) + parseInt(row.total)
					// 更新用户信息
					context?.$http({
						url: `${context?.$toolUtil.storageGet('frontSessionTable')}/update`,
						method: 'post',
						data: userinfo.value
					}).then(obj => {
						row.status = '已退款'
						// 修改订单状态
						context?.$http({
							url: 'orders/update',
							method: 'post',
							data: row
						}).then(res1 => {
							context?.$toolUtil.message('退款成功', 'success', () => {
								getSession()
								statusChange()
							})
						})
					})
				} else {
					// 如果是购物或者团购模式，且商品存在积分，则把加上的积分减去
					if (data.jf) {
						userinfo.value.jf = parseInt(userinfo.value.jf) - parseInt(row.total)
					}
					// 把减去的余额加回去
					userinfo.value.money = (parseFloat(userinfo.value.money) + parseFloat(row.total)).toFixed(2)
					// 更新用户信息
					context?.$http({
						url: `${context?.$toolUtil.storageGet('frontSessionTable')}/update`,
						method: 'post',
						data: userinfo.value
					}).then(obj => {
						row.status = '已退款'
						// 修改订单状态
						context?.$http({
							url: 'orders/update',
							method: 'post',
							data: row
						}).then(res1 => {
							context?.$toolUtil.message('退款成功', 'success', () => {
								getSession()
								statusChange()
							})
						})
					})
				}
			})
		})
	}
	// 退货
	const refundGoodClick = (row) => {
		ElMessageBox.confirm(`是否对该订单进行退货操作？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			context?.$http({
				url: `${row.tablename}/info/${row.goodid}`,
				method: 'get'
			}).then(res => {
				let data = res.data.data
				// 如果商品存在库存，则加回去
				if (data.alllimittimes) {
					data.alllimittimes = parseInt(data.alllimittimes) + parseInt(row.buynumber)
					// 更新商品库存
					context?.$http({
						url: `${row.tablename}/update`,
						method: 'post',
						data: data
					}).then(obj1 => {})
				}
				if (row.type == 2) {
					// 如果是积分兑换，则把减去的积分加回去
					userinfo.value.jf = parseInt(userinfo.value.jf) + parseInt(row.total)
					// 更新用户信息
					context?.$http({
						url: `${context?.$toolUtil.storageGet('frontSessionTable')}/update`,
						method: 'post',
						data: userinfo.value
					}).then(obj => {
						row.status = '已退款'
						// 修改订单状态
						context?.$http({
							url: 'orders/update',
							method: 'post',
							data: row
						}).then(res1 => {
							context?.$toolUtil.message('退款成功', 'success', () => {
								getSession()
								statusChange()
							})
						})
					})
				} else {
					// 如果是购物或者团购模式，且商品存在积分，则把加上的积分减去
					if (data.jf) {
						userinfo.value.jf = parseInt(userinfo.value.jf) - parseInt(row.total)
					}
					// 把减去的余额加回去
					userinfo.value.money = (parseFloat(userinfo.value.money) + parseFloat(row.total)).toFixed(2)
					// 更新用户信息
					context?.$http({
						url: `${context?.$toolUtil.storageGet('frontSessionTable')}/update`,
						method: 'post',
						data: userinfo.value
					}).then(obj => {
						row.status = '已退款'
						// 修改订单状态
						context?.$http({
							url: 'orders/update',
							method: 'post',
							data: row
						}).then(res1 => {
							context?.$toolUtil.message('退货成功', 'success', () => {
								getSession()
								statusChange()
							})
						})
					})
				}
			})
		})
	}
	//确认收货
	const confirmGoodClick = (row) => {
		ElMessageBox.confirm(`是否确认收货？`, '提示', {
			confirmButtonText: '是',
			cancelButtonText: '否',
			type: 'warning',
		}).then(() => {
			//直接完成支付，已减去用户余额，已减去库存，已累加积分，则不需要用户操作跟商品库存操作
			row.status = '已完成'
			//修改订单状态
			context?.$http({
				url: 'orders/update',
				method: 'post',
				data: row
			}).then(res1 => {
				context?.$toolUtil.message('确认收货成功', 'success', () => {
					getSession()
					statusChange()
				})
			})
		})
	}
	//物流
	const logisticsVisible = ref(false)
	const logisticsText = ref('')
	const logisticsClick = (row) => {
		logisticsText.value = row.logistics
		logisticsVisible.value = true
	}
	//去评论
	const toDetailClick = (row) => {
		router.push(`/index/${row.tablename}Detail?id=${row.goodid}`)
	}
	const userinfo = ref({})
	const getSession = () => {
		context?.$http({
			url: `${context?.$toolUtil.storageGet('frontSessionTable')}/session`,
			method: 'get'
		}).then(res => {
			userinfo.value = res.data.data
		})
	}
	const init = () => {
		getSession()
		getList()
	}
	init()
</script>

<style lang="scss" scoped>

	// 切换栏
	.demo-tabs {
		// 头部
		:deep(.el-tabs__header) {
			padding: 0 10px;
			box-shadow: none;
			margin: 20px auto 0px;
			background: none;
			width: 100%;
			border-color: #27bacc10;
			border-width: 0px;
			line-height: auto;
			border-style: solid;
			height: auto;
			// 滑动区
			.el-tabs__nav-scroll {
				padding: 0 15px;
				display: flex;
				justify-content: center;
				// list
				.el-tabs__nav {
					border: 0px solid #eee;
					// item
					.el-tabs__item {
						border-radius: 20px;
						padding: 0 20px;
						margin: 0 10px 0 0;
						color: #333;
						background: #eee;
						border-color: #b5b5b5;
						border-width: 1px;
						line-height: 40px;
						border-style: solid;
						transition: all 0s;
						height: 40px;
					}
					// item active
					.is-active {
						border-radius: 20px;
						padding: 0 20px;
						margin: 0 10px 0 0;
						color: #fff;
						background: #113961;
						border-color: #113961;
						border-width: 1px;
						line-height: 36px;
						border-style: solid;
						height: 36px;
					}
					// item hover
					.el-tabs__item:hover {
						padding: 0 20px;
						color: #fff;
						background: #113961;
						border-color: #113961;
						border-width: 1px;
						line-height: 36px;
						border-style: solid;
						height: 36px;
					}
				}
			}
		}
	}
	// 表格样式
	.el-table {
		padding: 0;
		margin: 30px 0 0;
		background: #fff;
		width: 100%;
		border-color: #eee;
		border-width: 1px 0 0 1px;
		border-style: solid;
		:deep(.el-table__header-wrapper) {
			thead {
				color: #999;
				font-weight: 500;
				width: 100%;
				tr {
					background: #fcfcfc;
					th {
						padding: 6px 0;
						background: none;
						border-color: #eee;
						border-width: 0 0px 1px 0;
						border-style: solid;
						text-align: left;
						.cell {
							padding: 0 10px;
							word-wrap: normal;
							color: #333;
							word-break: break-all;
							white-space: normal;
							font-weight: bold;
							display: inline-block;
							vertical-align: middle;
							width: 100%;
							line-height: 24px;
							position: relative;
							text-overflow: ellipsis;
							//未选中样式
							.el-checkbox {
								//复选框
								.el-checkbox__inner {
									background: #fff;
									border-color: #999;
								}
							}
							//选中样式
							.is-checked {
								//复选框
								.el-checkbox__inner {
									background: #113961;
									border-color: #113961;
								}
							}
						}
					}
				}
			}
		}
		:deep(.el-table__body-wrapper) {
			tbody {
				width: 100%;
				tr {
					background: #fff;
					td {
						padding: 12px 0;
						color: #999;
						background: #fff;
						border-color: #eee;
						border-width: 0 0px 1px 0;
						border-style: solid;
						text-align: left;
						.cell {
							padding: 0 10px;
							overflow: hidden;
							word-break: break-all;
							white-space: normal;
							line-height: 24px;
							text-overflow: ellipsis;
							// 支付
							.pay_btn {
								border: 0px solid #2da065;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 10px;
								margin: 0 5px 6px 0;
								outline: none;
								color: #113961;
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 支付-悬浮
							.pay_btn:hover {
							}
							// 取消订单
							.cancel_btn {
								border: 0px solid rgba(4,133,156,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 10px;
								margin: 0 5px 6px 0;
								outline: none;
								color: rgba(4,133,156,1);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 取消订单-悬浮
							.cancel_btn:hover {
							}
							// 退款
							.refundPrice_btn {
								border: 0px solid rgba(156,4,4,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 5px 6px 0;
								outline: none;
								color: rgba(156,4,4,.8);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 退款-悬浮
							.refundPrice_btn:hover {
							}
							// 退货
							.refundGood_btn {
								border: 0px solid rgba(156,4,4,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 10px 6px 0;
								outline: none;
								color: rgba(156,4,4,.8);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 退货-悬浮
							.refundGood_btn:hover {
							}
							// 物流
							.logistics_btn {
								border: 0px solid rgba(2,102,181,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 10px 6px 0;
								outline: none;
								color: rgba(2,102,181,1);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 物流-悬浮
							.logistics_btn:hover {
							}
							// 确认收货
							.confirm_btn {
								border: 0px solid rgba(2,102,181,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 10px;
								margin: 0 10px 6px 0;
								outline: none;
								color: rgba(2,102,181,1);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 确认收货-悬浮
							.confirm_btn:hover {
							}
							// 评论
							.toDetail_btn {
								border: 0px solid rgba(2,102,181,.3);
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 10px 6px 0;
								outline: none;
								color: rgba(2,102,181,1);
								background: none;
								width: auto;
								font-size: 13px;
								height: 24px;
							}
							// 评论-悬浮
							.toDetail_btn:hover {
							}
							//未选中样式
							.el-checkbox {
								//复选框
								.el-checkbox__inner {
									background: #fff;
									border-color: #999;
								}
							}
							//选中样式
							.is-checked {
								//复选框
								.el-checkbox__inner {
									background: #113961;
									border-color: #113961;
								}
							}
						}
					}
				}
				tr:hover {
					td {
						padding: 12px 0;
						color: #333;
						background: #fcfcfc;
						border-color: #eee;
						border-width: 0 0px 1px 0;
						border-style: solid;
						text-align: left;
					}
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
</style>