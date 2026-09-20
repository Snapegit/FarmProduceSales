<template>
	<div class="app-contain" :style='{"width":"64%","padding":"20px 40px","margin":"30px auto 50px","position":"relative","borderRadius":"0px","background":"#fff"}'>
		<div class="back_view" v-if="centerType">
			<el-button class="back_btn" @click="backClick" type="primary">返回</el-button>
		</div>
		<div class="section_title">
			{{formName}}
		</div>
		<div class="search_btn_view">
			<el-button class="add_btn" @click="addClick">
				新增{{formName}}
			</el-button>
		</div>
		<div class="list_bottom">
			<div class="data_box">
				<div class="data_view">
					<el-table v-loading="listLoading" border :stripe='false' @selection-change="handleSelectionChange" ref="table"
						:data="list" @row-click="listChange">
						<el-table-column type="selection" width="55" :resizable='true' align="left" header-align="left" />
						<el-table-column label="序号" width="120" :resizable='true' align="left" header-align="left">
							<template #default="scope">{{ scope.$index + 1}}</template>
						</el-table-column>
						<el-table-column label="地址" :resizable='true' align="left" header-align="left">
							<template #default="scope">
								{{scope.row.address}}
							</template>
						</el-table-column>
						<el-table-column label="收货人" :resizable='true' align="left" header-align="left">
							<template #default="scope">
								{{scope.row.name}}
							</template>
						</el-table-column>
						<el-table-column label="电话" :resizable='true' align="left" header-align="left">
							<template #default="scope">
								{{scope.row.phone}}
							</template>
						</el-table-column>
						<el-table-column label="是否默认地址" :resizable='true' align="left" header-align="left">
							<template #default="scope">
								<el-switch v-model="scope.row.isdefault" class="ml-2"
									style="--el-switch-on-color: $template2.front.list.table.tbody.td.switch.onColor; --el-switch-off-color: $template2.front.list.table.tbody.td.switch.offColor" @change="(e)=>isdefaultChange(e,scope.row)" active-value="是" inactive-value="否" />
							</template>
						</el-table-column>
						<el-table-column label="操作" :resizable='true' align="left" header-align="left">
							<template #default="scope">
								<el-button class="edit_btn" @click="editClick(scope.row.id)" type="primary" size="default">修改</el-button>
								<el-button class="del_btn" @click="delClick(scope.row.id)" type="danger" size="default">删除</el-button>
							</template>
						</el-table-column>
					</el-table>
				</div>
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
			</div>
		</div>
		<formModel ref="formModelRef" @formModelChange="formModelChange"></formModel>
	</div>
</template>

<script setup>
	import formModel from './formModel'
	import {
		ref,
		nextTick,
		getCurrentInstance
	} from 'vue';
	import {
		ElMessageBox
	} from 'element-plus'
	import {
		useRoute,
		useRouter
	} from 'vue-router'
	const context = getCurrentInstance()?.appContext.config.globalProperties;
	//基础信息
	const tableName = 'address'
	const formName = '地址'
	const router = useRouter()
	const route = useRoute()
	//基础信息
	//权限验证
	const btnAuth = (e, a) => {
		return context?.$toolUtil.isAuth(e, a)
	}
	const list = ref([])
	const listLoading = ref(false)
	const listQuery = ref({
		page: 1,
		limit: 20,
		userid: context?.$toolUtil.storageGet('userid')
	})
	const selRows = ref([])
	const searchQuery = ref({})
	const table = ref(null)
	const searchClick = () => {
		listQuery.value.page = 1
		getList()
	}
	const getList = () => {
		listLoading.value = true
		let params = JSON.parse(JSON.stringify(listQuery.value))
		context?.$http({
			url: `address/list`,
			method: 'get',
			params: params
		}).then(res => {
			listLoading.value = false
			list.value = res.data.data.list
			total.value = res.data.data.total
		})
	}
	//定义弹窗
	const formModelRef = ref(null)
	const formModelChange = () =>{
		listQuery.value.page = 1
		getList()
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
	//新增
	const addClick = () => {
		formModelRef.value.init(null,'新增' + formName)
	}
	//修改
	const editClick = (id = null) => {
		if (id) {
			formModelRef.value.init(id,'修改' + formName)
		}
	}
	const delClick = (id = null) => {
		if (id) {
			ElMessageBox.confirm(`是否删除选中${formName}`, '提示', {
				confirmButtonText: '是',
				cancelButtonText: '否',
				type: 'warning',
			}).then(() => {
				context?.$http({
					url: `address/delete`,
					method: 'post',
					data: [id]
				}).then(res => {
					context?.$toolUtil.message('删除成功', 'success', () => {
						getList()
					})
				})
			})
		}
	}
	const isdefaultChange = (e,row) =>{
		context?.$http({
			url: `address/update`,
			method: 'post',
			data: row
		}).then(res => {
			getList()
		})
	}
	//多选
	const handleSelectionChange = (e) => {
		selRows.value = e
	}
	//判断是否从个人中心跳转
	const centerType = ref(false)
	//返回
	const backClick = () => {
		router.push(`/index/${context?.$toolUtil.storageGet('frontSessionTable')}Center`)
	}
	const init = () => {
		if (route.query.centerType) {
			centerType.value = true
		}
		getList()
	}
	init()
</script>

<style lang="scss">
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
	// 数据盒子
	.list_bottom {
		border-radius: 0;
		padding: 20px 0 0;
		margin: 20px 0 0;
		background: #fff;
		display: flex;
		width: calc(100% - 0px);
		align-items: flex-start;
		flex-wrap: wrap;
		.data_box {
			border: 0px solid #ddd;
			padding: 0;
			background: none;
			width: 100%;
			.data_view {
				border: 0px solid #eee;
				padding: 0 0 0 20px;
				overflow: hidden;
				background: none;
				display: flex;
				width: 100%;
				flex-wrap: wrap;
			}
		}
	}
	.search_btn_view {
		margin: 20px 0;
		display: flex;
		// 新增按钮
		.add_btn {
			border: 1px solid #333;
			cursor: pointer;
			border-radius: 4px;
			padding: 0 20px;
			margin: 0 10px 0 0;
			outline: none;
			color: #fff;
			background: #333;
			width: auto;
			font-size: 14px;
			transition: all 0.3s;
			height: 36px;
		}
		// 新增按钮-悬浮
		.add_btn:hover {
			transform: rotate(360deg);
		}
	}
	
	// 表格样式
	.el-table {
		padding: 0;
		margin: 10px 30px 10px 10px;
		background: #fff;
		width: calc(100% - 40px);
		border-color: #eee;
		border-width: 1px 0 0 1px;
		border-style: solid;
		:deep(.el-table__header-wrapper) {
			thead {
				color: #999;
				background: #f6f6f6;
				font-weight: 500;
				width: 100%;
				tr {
					background: #f6f6f6;
					th {
						padding: 12px 0;
						background: none;
						border-color: #eee;
						border-width: 0 1px 1px 0;
						border-style: solid;
						text-align: left;
						.cell {
							padding: 0 10px;
							word-wrap: normal;
							color: #666;
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
									border-color: #ccc;
								}
							}
							//选中样式
							.is-checked {
								//复选框
								.el-checkbox__inner {
									background: #55ffff;
									border-color: #55ffff;
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
						color: #666;
						background: #fff;
						border-color: #eee;
						border-width: 0 1px 1px 0;
						border-style: solid;
						text-align: left;
						.cell {
							padding: 0 10px;
							overflow: hidden;
							word-break: break-all;
							white-space: normal;
							line-height: 24px;
							text-overflow: ellipsis;
							// 修改
							.edit_btn {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 4px 4px 0;
								color: #fff;
								background: #11396199;
								width: auto;
								font-size: 14px;
								transition: all 0.3s;
								height: 30px;
							}
							// 修改-悬浮
							.edit_btn:hover {
								transform: rotate(360deg);
							}
							// 删除
							.del_btn {
								border: 0;
								cursor: pointer;
								border-radius: 4px;
								padding: 0 20px;
								margin: 0 4px 4px 0;
								color: #fff;
								background: #eb3f3f99;
								width: auto;
								font-size: 14px;
								transition: all 0.3s;
								height: 30px;
							}
							// 删除-悬浮
							.del_btn:hover {
								transform: rotate(360deg);
							}
							//未选中样式
							.el-checkbox {
								//复选框
								.el-checkbox__inner {
									background: #fff;
									border-color: #ccc;
								}
							}
							//选中样式
							.is-checked {
								//复选框
								.el-checkbox__inner {
									background: #55ffff;
									border-color: #55ffff;
								}
							}
						}
					}
				}
				tr:hover {
					td {
						padding: 12px 0;
						color: #333;
						background: #f8f8f8;
						border-color: #eee;
						border-width: 0 1px 1px 0;
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