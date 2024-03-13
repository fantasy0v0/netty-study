package com.github.fantasy0v0.netty.vo;

import io.netty.buffer.ByteBuf;

/**
 * 服务消息
 */
public class ServiceMessage {

  /**
   * 消息类型 0 请求 1 响应
   */
  private byte type;

  /**
   * 消息编号
   */
  private short id;

  /**
   * 业务类型
   */
  private short businessType;

  /**
   * 业务数据
   */
  private ByteBuf data;

}
